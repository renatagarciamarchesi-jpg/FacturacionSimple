using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//REDORDATORIO: Un DTO (Data Transfer Object o Objeto de Transferencia de Datos) es un patrón de diseño
//que consiste en una clase simple cuya única responsabilidad es transportar datos entre diferentes capas
//o componentes de una aplicación, sin contener lógica de negocio compleja.

namespace FacturacionSimple.Entidades
{    
    public class Producto
    {
        public int Id { get; set; } //Corresponde a la Primary Key
        public string Codigo { get; set; } = string.Empty;   //Corresponde al código de barras.
                                                             //Iniciamos como un string vacío
                                                             //(Evita null reference exception)
        public string Nombre { get; set; } = string.Empty;  //Nombre del producto.
                                                            //Iniciamos como un string vacío
                                                            //(Evita null reference exception)
        public decimal Precio { get; set; }
        public bool Activo { get; set; } = true;    //Por defecto, el producto siempre inicia como activo

        public override string ToString()
        {
            // Se usa como texto por defecto en el combo de productos
            return $"{Codigo} - {Nombre} : {Precio}: $0.00";
        }
    }
}
