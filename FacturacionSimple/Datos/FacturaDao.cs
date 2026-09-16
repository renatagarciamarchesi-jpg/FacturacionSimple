using FacturacionSimple.Entidades;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FacturacionSimple.Datos
{
    //RECORDATORIO: Los DAO (Data Access Objects u Objetos de Acceso a Datos) son patrones de diseño que
    //proporcionan una interfaz abstracta para acceder a bases de datos, ocultando la lógica de persistencia
    //al resto de la aplicación. 
    internal class FacturaDao
    {
        //Listado de cabeceras para la pantalla de consulta, con filtros
        //opcionales por rango de fechas y/o texto de cliente
        public List<Factura> Listar(DateTime? desde, DateTime? hasta, string? textoCliente)
        {
            List<Factura> facturas = new List<Factura>();

            try
            {
                using SqlConnection conexion = Conexion.Crear();    //Se crea la conexión a la BD
                conexion.Open();    //Se abre

                //Usamos using para asegurarnos de cerrar la conexión cuando no sea necesaria y liberar recursos
                using SqlCommand comando = new SqlCommand(
                    """
                    SELECT Id, Numero, Fecha, ClienteNombre, ClienteDocumento, Total, Anulada
                    FROM Facturas
                    WHERE (@Desde IS NULL OR Fecha >= @Desde)
                      AND (@Hasta IS NULL OR Fecha <= @Hasta)
                      AND (@Texto IS NULL OR ClienteNombre LIKE @Texto)
                    ORDER BY Numero DESC
                    """,
                    conexion);  //Se crea el comando SQL con la consulta parametrizada

                comando.Parameters.AddWithValue("@Desde", (object?)desde ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Hasta", (object?)hasta ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Texto", string.IsNullOrWhiteSpace(textoCliente) ? DBNull.Value : $"%{textoCliente}%");

                using SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    facturas.Add(MapearCabecera(lector));
                }
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }

            return facturas;
        }

        //Cabecera + detalle en solo lectura, para el panel de consulta
        public Factura? ObtenerConDetalle(int id)
        {
            try
            {
                using SqlConnection conexion = Conexion.Crear();
                conexion.Open();

                //Paso 1: traer la cabecera
                Factura? factura = null;

                using (SqlCommand comando = new SqlCommand(
                    """
                SELECT Id, Numero, Fecha, ClienteNombre, ClienteDocumento, Total, Anulada
                FROM Facturas WHERE Id = @Id
                """,
                    conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);

                    using SqlDataReader lector = comando.ExecuteReader();

                    if (lector.Read())
                    {
                        factura = MapearCabecera(lector);
                    }
                }

                //Si no existe la factura, no tiene sentido seguir
                if (factura is null)
                {
                    return null;
                }

                //Paso 2: traer las líneas de esa factura
                using (SqlCommand comando = new SqlCommand(
                    """
                SELECT d.Id, d.FacturaId, d.ProductoId, p.Codigo AS ProductoCodigo, p.Nombre AS ProductoNombre, d.Cantidad, d.PrecioUnitario, d.Subtotal
                FROM FacturaDetalle d
                INNER JOIN Productos p ON p.Id = d.ProductoId
                WHERE d.FacturaId = @FacturaId
                ORDER BY d.Id
                """, conexion))
                {
                    comando.Parameters.AddWithValue("@FacturaId", id);

                    using SqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        factura.Detalle.Add(new FacturaDetalle
                        {
                            Id = (int)lector["Id"],
                            FacturaId = (int)lector["FacturaId"],
                            ProductoId = (int)lector["ProductoId"],
                            ProductoCodigo = lector["ProductoCodigo"].ToString() ?? string.Empty,
                            ProductoNombre = lector["ProductoNombre"].ToString() ?? string.Empty,
                            Cantidad = (int)lector["Cantidad"],
                            PrecioUnitario = (decimal)lector["PrecioUnitario"]
                        }
                        );
                    }
                }

                return factura;
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }
        }

        //Emisión de factura: cabecera + detalle en una sola transacción.
        //Si falla cualquier INSERT, se hace rollback completo y no queda
        //nada grabado (ATOMIC)
        public int Insertar(Factura factura)
        {
            if (factura.Detalle.Count == 0)
            {
                throw new InvalidOperationException("La factura debe tener al menos una línea.");
            }

            using SqlConnection conexion = Conexion.Crear();
            conexion.Open();

            using SqlTransaction transaccion = conexion.BeginTransaction();
            
            try
            {
                factura.RecalcularTotal();

                int numero = ObtenerSiguienteNumero(conexion, transaccion);

                //1. Insertar la cabecera y recuperar el id que le asignó la BD
                int facturaId;

                using (SqlCommand comando = new SqlCommand(
                    """
                INSERT INTO Facturas (Numero, Fecha, ClienteNombre, ClienteDocumento, Total, Anulada)
                VALUES (@Numero, @Fecha, @ClienteNombre, @ClienteDocumento, @Total, 0);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
                """,
                    conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("@Numero", numero);
                    comando.Parameters.AddWithValue("@Fecha", factura.Fecha.Date);
                    comando.Parameters.AddWithValue("@ClienteNombre", factura.ClienteNombre);
                    comando.Parameters.AddWithValue("@ClienteDocumento", factura.ClienteDocumento);
                    comando.Parameters.AddWithValue("@Total", factura.Total);

                    facturaId = (int)comando.ExecuteScalar();
                }

                //2. Insertar cada línea de detalle, usando el id de la cabecera
                foreach (FacturaDetalle linea in factura.Detalle)
                {
                    using SqlCommand comando = new SqlCommand(
                        """
                    INSERT INTO FacturaDetalle (FacturaId, ProductoId, Cantidad, PrecioUnitario, Subtotal)
                    VALUES (@FacturaId, @ProductoId, @Cantidad, @PrecioUnitario, @Subtotal)
                    """,
                        conexion, transaccion);
                    comando.Parameters.AddWithValue("@FacturaId", facturaId);
                    comando.Parameters.AddWithValue("@ProductoId", linea.ProductoId);
                    comando.Parameters.AddWithValue("@Cantidad", linea.Cantidad);
                    comando.Parameters.AddWithValue("@PrecioUnitario", linea.PrecioUnitario);
                    comando.Parameters.AddWithValue("@Subtotal", linea.Subtotal);

                    comando.ExecuteNonQuery();
                }

                //3. Si llegamos hasta acá sin excepciones, confirmamos todo junto (commit)
                transaccion.Commit();

                factura.Id = facturaId;
                factura.Numero = numero;
                return numero;
            }
            catch (SqlException ex)
            {
                //Si algo falló. Deshacemos cabecera + detalle y avisamos con un mensaje entendible.
                transaccion.Rollback();
                throw ManejadorErroresSql.Traducir(ex);
            }
            catch
            {
                //Por cualquier otro posible error
                transaccion.Rollback();
                throw;
            }
        }

        //Calcula el próximo número de factura como "el máximo actual + 1".
        //Se hace dentro de la misma transacción de Insertar, así el número
        //que se calcula acá es el que efectivamente se graba.
        private static int ObtenerSiguienteNumero(SqlConnection conexion, SqlTransaction transaccion)
        {
            using SqlCommand comando = new SqlCommand(
                "SELECT ISNULL(MAX(Numero), 0) + 1 FROM Facturas", conexion, transaccion);

            return (int)comando.ExecuteScalar();
        }

        //Anulación por estado en lugar de DELETE, para no perder el
        //historial. El "AND Anulada = 0" en el UPDATE es lo que impide
        //anular dos veces: si ya estaba anulada, no actualiza ninguna fila.
        public void Anular(int id)
        {
            try
            {
                using SqlConnection conexion = Conexion.Crear();
                conexion.Open();

                using SqlCommand comando = new SqlCommand(
                    "UPDATE Facturas SET Anulada = 1 WHERE Id = @Id AND Anulada = 0", conexion);
                
                comando.Parameters.AddWithValue("@Id", id);

                int filasAfectadas = comando.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    throw new InvalidOperationException("La factura no existe ó ya estaba anulada.");
                }
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }
        }

        //Informe simple: agrupa las líneas de factura por producto y suma
        //cantidad y monto, para un rango de fechas.
        public List<ItemInforme> InformePorProducto(DateTime desde, DateTime hasta)
        {
            List<ItemInforme> items = new();

            try
            {
                using SqlConnection conexion = Conexion.Crear();
                conexion.Open();

                using SqlCommand comando = new SqlCommand(
                    """
                SELECT p.Id AS ProductoId, p.Codigo, p.Nombre,
                       SUM(d.Cantidad) AS CantidadFacturada,
                       SUM(d.Subtotal) AS MontoFacturado
                FROM FacturaDetalle d
                INNER JOIN Facturas f ON f.Id = d.FacturaId
                INNER JOIN Productos p ON p.Id = d.ProductoId
                WHERE f.Fecha BETWEEN @Desde AND @Hasta
                  AND f.Anulada = 0
                GROUP BY p.Id, p.Codigo, p.Nombre
                ORDER BY MontoFacturado DESC
                """,
                    conexion);

                comando.Parameters.AddWithValue("@Desde", desde.Date);
                comando.Parameters.AddWithValue("@Hasta", hasta.Date);

                using SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    items.Add(new ItemInforme
                    {
                        ProductoId = (int)lector["ProductoId"],
                        ProductoCodigo = lector["Codigo"].ToString() ?? string.Empty,
                        ProductoNombre = lector["Nombre"].ToString() ?? string.Empty,
                        CantidadFacturada = (int)lector["CantidadFacturada"],
                        MontoFacturado = (decimal)lector["MontoFacturado"]
                    });
                }
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }

            return items;
        }

        //Convierte una fila del lector (cabecera de factura) en un objeto Factura. 
        private static Factura MapearCabecera(SqlDataReader lector)
        {
            return new Factura
            {
                Id = (int)lector["Id"],
                Numero = (int)lector["Numero"],
                Fecha = (DateTime)lector["Fecha"],
                ClienteNombre = lector["ClienteNombre"].ToString() ?? string.Empty,
                ClienteDocumento = lector["ClienteDocumento"].ToString() ?? string.Empty,
                Total = (decimal)lector["Total"],
                Anulada = (bool)lector["Anulada"]
            };
        }
    }
}