using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

//REDORDATORIO: Un DTO (Data Transfer Object o Objeto de Transferencia de Datos) es un patrón de diseño
//que consiste en una clase simple cuya única responsabilidad es transportar datos entre diferentes capas
//o componentes de una aplicación, sin contener lógica de negocio compleja.

namespace FacturacionSimple.Entidades
{
    public class Factura
    {
        public int Id { get; set; } //Primary Key
        public int Numero { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;   //Se establece la fecha según la misma fecha de emisión
        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteDocumento { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public bool Anulada { get; set; }   //Permite anular una factura (No se borra de la BD, solo se anula)
        public List<FacturaDetalle> Detalle { get; set; } = new List<FacturaDetalle>();

        public void RecalcularTotal()   //Calcula recaudación total
        {
            Total = Detalle.Sum(linea => linea.Subtotal);   //Usamos LINQ para agilizar la suma
        }
    }
}
