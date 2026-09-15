using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

//REDORDATORIO: Un DTO (Data Transfer Object o Objeto de Transferencia de Datos) es un patrón de diseño
//que consiste en una clase simple cuya única responsabilidad es transportar datos entre diferentes capas
//o componentes de una aplicación, sin contener lógica de negocio compleja.

//ItemInforme es un DTO de lectura que recopila los datos esenciales de los productos y las facturas
//para su fácil acceso sin comprometer a las clases o las bases de datos. (Uso para informes.)

namespace FacturacionSimple.Entidades
{
    public class ItemInforme
    {
        public int ProductoId { get; set; } //Producto
        public string ProductoCodigo { get; set; } = string.Empty;  //Clase Producto
        public string ProductoNombre { get; set; } = string.Empty;  //Clase Producto
        public int CantidadFacturada { get; set; }  //Clase Factura
        public decimal MontoFacturado { get; set; } //Clase Factura
    }
}
