using AppFiplasto.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppFiplasto.Droid.WebServices))]

namespace AppFiplasto.Droid
{
    public class WebServices : IValido
    {
     

        public DataSet Biomasa()
        {
            var servicio = new WebServiceXamarin.WebService();
            DataSet retorno = servicio.RemitosBiomasa();
            return retorno;
        }

        public bool GuardarRegistro(string ticket,string turno, string grupo,string fechapicado)
        {
            var servicio = new WebServiceXamarin.WebService();
            bool retorno = servicio.GrabarTicketBio(ticket, turno, grupo,fechapicado);
            return retorno;
        }


        public bool AutorizaRQ(string esquemaAutorizacion, string modfor, string codfor, string nrofor, string user)
        {
            var servicio = new WebServiceXamarin.WebService();
            bool retorno = servicio.AutorizaRQ(esquemaAutorizacion, modfor, codfor, nrofor, user);
            return retorno;
        }




        public bool LoginWindows(string dominio, string user, string pass)
        {
            var servicio = new WebServiceSpif.Service1();
            bool retorno = servicio.IsAuthenticated("Fiplasto", user, pass);
            return retorno;
        }

        public DataSet Permisos(string usuario)
        {
            var servicio = new WebServiceXamarin.WebService();
            DataSet retorno = servicio.Permisos(usuario);
            return retorno;
        }


        public DataSet RQPendientes(string esquemaAutorizacion)
        {
            var servicio = new WebServiceXamarin.WebService();
            DataSet retorno = servicio.RQPendientes(esquemaAutorizacion);
            return retorno;
        }


        public DataSet StockMadera()
        {
            var servicio = new WebServiceXamarin.WebService();
            DataSet retorno = servicio.ListaStockMadera();
            return retorno;
        }

        public DataSet Produccion(string UltimoDiaMesAnt,string FchHasta)
        {
            var servicio = new WebServiceXamarin.WebService();
            DataSet retorno = servicio.InformeProduccion(UltimoDiaMesAnt,FchHasta);
            return retorno;
        }
    }
}