using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AppFiplasto.Models
{
   public class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

        public DataSet Datos{ get; set; }

    }
}
