using System;
using System.Collections.Generic;
using System.Text;

namespace AppFiplasto.Models
{
    public class Produccion
    {

        public DateTime Fecha { get; set; }
        public float Linea{ get; set; }
        public float Prensada { get; set; }
        public string Producto{ get; set; }
        public float   Factor{ get; set; }
        public float Aberturas { get; set; }
               
    }
}
