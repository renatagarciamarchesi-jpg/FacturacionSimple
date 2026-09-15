using Microsoft.Data.SqlClient;

namespace FacturacionSimple.Datos
{
    //Traduce las excepciones técnicas de ADO.NET/SQL Server a mensajes
    //entendibles para mostrar en la interfaz, distinguiendo al menos error de
    //conexión y violación de unique/FK
    internal static class ManejadorErroresSql
    {
        // Números de error de SQL Server más comunes en el TP
        private const int ViolacionUniqueIndex = 2601;
        private const int ViolacionUniqueConstraint = 2627;
        private const int ViolacionForeignKey = 547;

        public static Exception Traducir(SqlException ex)
        {
            if (ex.Number == ViolacionUniqueIndex || ex.Number == ViolacionUniqueConstraint)
            {
                return new InvalidOperationException("Ya existe un registro con ese valor único (por ejemplo, un código de producto repetido).", ex);
            }

            if (ex.Number == ViolacionForeignKey)
            {
                return new InvalidOperationException("La operación no se puede completar porque el registro está referenciado por otros datos.", ex);
            }

            if (EsErrorDeConexion(ex))
            {
                return new InvalidOperationException("No se pudo conectar con la base de datos. Verifique que SQL Server esté disponible y la cadena de conexión.", ex);
            }

            return new InvalidOperationException($"Error de base de datos: {ex.Message}", ex);
        }

        private static bool EsErrorDeConexion(SqlException ex)
        {
            // 2 = no se encontró el servidor; 53 = ruta de red no encontrada;
            // -1/-2 = timeout de conexión/comando; 18456 = login fallido.
            int[] numerosDeConexion = { 2, 53, -1, -2, 18456 };
            return numerosDeConexion.Contains(ex.Number);
        }
    }
}
