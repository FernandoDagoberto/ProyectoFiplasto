using System;
using System.Collections.Generic;
using System.Text;

namespace AppFiplasto.Models
{
    public class RQHPendientes
    {
        public string CORMVH_MODFOR{ get; set; }
        public string CORMVH_CODFOR{ get; set; }
        public int CORMVH_NROFOR { get; set; }
        public DateTime CORMVH_FCHMOV { get; set; }
        public string CORMVH_TEXTOS{ get; set; }
        public string CORMVH_COFLIS{ get; set; }
        public float SINIMP{ get; set; }
                   
        private string precio;

        public string Precio 
        {
            get { return $"{CORMVH_COFLIS} {SINIMP}"; }

            set { precio = value; }
        }

    }
}
