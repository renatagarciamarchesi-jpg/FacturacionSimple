using FacturacionSimple.Entidades;
using Microsoft.Data.SqlClient;

namespace FacturacionSimple.Datos
{
    internal class ProductoDao
    {
        private const string ColumnasSelect = "Id, Codigo, Nombre, Precio, Activo";

        public List<Producto> Listar()
        {
            List<Producto> productos = new();

            try
            {
                using SqlConnection conexion = Conexion.Crear();

                conexion.Open();

                using SqlCommand comando = new SqlCommand($"SELECT {ColumnasSelect} FROM Productos ORDER BY Nombre", conexion);

                using SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    productos.Add(Mapear(lector));
                }
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }

            return productos;
        }

        public List<Producto> ListarActivos()
        {
            return Listar().Where(p => p.Activo).ToList();
        }

        //Búsqueda por código o nombre (LIKE), usada en el listado del ABM
        public List<Producto> Buscar(string texto)
        {
            List<Producto> productos = new();

            try
            {
                using SqlConnection conexion = Conexion.Crear();

                conexion.Open();

                using SqlCommand comando = new SqlCommand(
                    $"""
                SELECT {ColumnasSelect} FROM Productos
                WHERE Codigo LIKE @Texto OR Nombre LIKE @Texto
                ORDER BY Nombre
                """,
                    conexion);

                comando.Parameters.AddWithValue("@Texto", $"%{texto}%");

                using SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    productos.Add(Mapear(lector));
                }
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }

            return productos;
        }

        public Producto? ObtenerPorId(int id)
        {
            try
            {
                using SqlConnection conexion = Conexion.Crear();

                conexion.Open();

                using SqlCommand comando = new SqlCommand($"SELECT {ColumnasSelect} FROM Productos WHERE Id = @Id", conexion);
                
                comando.Parameters.AddWithValue("@Id", id);

                using SqlDataReader lector = comando.ExecuteReader();

                return lector.Read() ? Mapear(lector) : null;
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }
        }

        public bool ExisteCodigo(string codigo, int? excluirId = null)
        {
            try
            {
                using SqlConnection conexion = Conexion.Crear();

                conexion.Open();

                using SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Productos WHERE Codigo = @Codigo AND (@ExcluirId IS NULL OR Id <> @ExcluirId)",
                    conexion);

                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@ExcluirId", (object?)excluirId ?? DBNull.Value);

                int cantidad = (int)comando.ExecuteScalar();

                return cantidad > 0;
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }
        }

        public int Insertar(Producto producto)
        {
            try
            {
                using SqlConnection conexion = Conexion.Crear();

                conexion.Open();

                using SqlCommand comando = new SqlCommand(
                    """
                INSERT INTO Productos (Codigo, Nombre, Precio, Activo)
                VALUES (@Codigo, @Nombre, @Precio, @Activo);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
                """,
                    conexion);

                AgregarParametros(comando, producto);

                return (int)comando.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }
        }

        public void Modificar(Producto producto)
        {
            try
            {
                using SqlConnection conexion = Conexion.Crear();

                conexion.Open();

                using SqlCommand comando = new SqlCommand(
                    """
                UPDATE Productos
                SET Codigo = @Codigo, Nombre = @Nombre, Precio = @Precio, Activo = @Activo
                WHERE Id = @Id
                """,
                    conexion);

                comando.Parameters.AddWithValue("@Id", producto.Id);
                AgregarParametros(comando, producto);

                comando.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }
        }

        //Baja lógica: se usa siempre que el producto ya fue facturado alguna vez
        public void DarDeBaja(int id)
        {
            try
            {
                using SqlConnection conexion = Conexion.Crear();

                conexion.Open();

                using SqlCommand comando = new SqlCommand("UPDATE Productos SET Activo = 0 WHERE Id = @Id", conexion);

                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }
        }

        //Un producto solo puede borrarse físicamente si nunca fue facturado
        public bool TieneDetalles(int productoId)
        {
            try
            {
                using SqlConnection conexion = Conexion.Crear();

                conexion.Open();

                using SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM FacturaDetalle WHERE ProductoId = @ProductoId", conexion);
                
                comando.Parameters.AddWithValue("@ProductoId", productoId);

                int cantidad = (int)comando.ExecuteScalar();

                return cantidad > 0;
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using SqlConnection conexion = Conexion.Crear();

                conexion.Open();

                using SqlCommand comando = new SqlCommand("DELETE FROM Productos WHERE Id = @Id", conexion);

                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ManejadorErroresSql.Traducir(ex);
            }
        }

        private static void AgregarParametros(SqlCommand comando, Producto producto)
        {
            comando.Parameters.AddWithValue("@Codigo", producto.Codigo);
            comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
            comando.Parameters.AddWithValue("@Precio", producto.Precio);
            comando.Parameters.AddWithValue("@Activo", producto.Activo);
        }

        private static Producto Mapear(SqlDataReader lector)
        {
            return new Producto
            {
                Id = (int)lector["Id"],
                Codigo = lector["Codigo"].ToString() ?? string.Empty,
                Nombre = lector["Nombre"].ToString() ?? string.Empty,
                Precio = (decimal)lector["Precio"],
                Activo = (bool)lector["Activo"]
            };
        }
    }
}
