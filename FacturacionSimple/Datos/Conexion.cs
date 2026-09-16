using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace FacturacionSimple.Datos
{
    //RECORDATORIO: Los DAO (Data Access Objects u Objetos de Acceso a Datos) son patrones de diseño que
    //proporcionan una interfaz abstracta para acceder a bases de datos, ocultando la lógica de persistencia
    //al resto de la aplicación. 
    internal static class Conexion
    {
        //ATENCIÓN: Preguntarle profe a qué cadena de conexión conectarme
        //(LocalDB es la opción por defecto que trae Visual Studio)
        public static string CadenaConexion =>
            @"Server=(localdb)\mssqllocaldb;Database=FacturacionSimple;Integrated Security=true;TrustServerCertificate=true;";
            //Se ocupa de ajustar la BD a la versión de SQL Server que tengamos instalada

        public static SqlConnection Crear() //Abre la conexión a la BD
        {
            return new SqlConnection(CadenaConexion); //Conexión entre BD y el programa en C#
                                                      //Sabe la dirección gracias a CadenaConexión
        }
    }
}
