using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

//REDORDATORIO: Un DTO (Data Transfer Object o Objeto de Transferencia de Datos) es un patrón de diseño
//que consiste en una clase simple cuya única responsabilidad es transportar datos entre diferentes capas
//o componentes de una aplicación, sin contener lógica de negocio compleja.

// Representa una línea de la factura (Producto individual de la factura)

namespace FacturacionSimple.Entidades
{
    public class FacturaDetalle
    {
        public int Id { get; set; } //Corresponde a la Primary Key
        public int FacturaId { get; set; }  //Foreign Key (Clase Factura) que hace referencia a la factura
        public int ProductoId { get; set; } //Foreign Key (Clase Producto) que hace referencia al producto
        public string ProductoCodigo { get; set; } = string.Empty;   //Iniciamos como un string vacío.
                                                                    //(Evita null reference exception)
        public string ProductoNombre { get; set; } = string.Empty;  //"" ""
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; } //Precio al facturar

        //El precio es copiado en el momento de factura (Si es modificado, no afecta la factura ya emitida)
        //RECORDAR: Los valores son tomados en la línea leída, NO se relee Producto.
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
