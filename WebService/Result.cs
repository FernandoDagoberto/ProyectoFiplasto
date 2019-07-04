using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebService
{
    public class Result
    {
        public string Error { get; set; }

        public bool ValidUser { get; set; }
        
        public DataSet Datos { get; set; }

        public string Mensaje { get; set; }
    }
}