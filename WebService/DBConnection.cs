using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService
{
    public class DBConnection
    {
        public string ConnectionString
        {
            get
            {
                string _DB = "fiplanew";
                string _servidor = "2.2.2.1";
                string _usuario = "sa";
                string _password = "";
                return String.Format("Server={0};Database={1};User Id={2};Password={3};", _servidor, _DB, _usuario, _password);

            }
        }



      
             
    

        public string ConnectionFiplasto
        {
            get
            {
                string _DB = "Fiplasto";
                string _servidor = "10.10.0.220";
                string _usuario = "sa";
                string _password = "Cualquiera123";
                return String.Format("Server={0};Database={1};User Id={2};Password={3};", _servidor, _DB, _usuario, _password);

            }
        }

        public string ConnectionCwSgCore
        {
            get
            {
                string _DB = "cwSGCore";
                string _servidor = "10.10.0.220";
                string _usuario = "sa";
                string _password = "Cualquiera123";
                return String.Format("Server={0};Database={1};User Id={2};Password={3};", _servidor, _DB, _usuario, _password);

            }
        }


        public string ConnectionNoticias
        {
            get
            {
                string _DB = "NOTICIAS";
                string _servidor = "2.2.2.1";
                string _usuario = "sa";
                string _password = "";
                return String.Format("Server={0};Database={1};User Id={2};Password={3};", _servidor, _DB, _usuario, _password);

            }
        }



        public string ConexAccess
        {
            get
            {
                //string _DB =@"\\por01\c$\Archivos de programa\Galil\data.mdb";
                string _password = "";
                return String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\\\2.2.2.76\\Galil\\data.mdb;Persist Security Info=true;");
                
            }
        }
              

    }
}