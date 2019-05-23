using AppFiplasto.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace AppFiplasto
{
   public interface IValido
    {
        bool LoginWindows(string dominio, string user, string pass);

        DataSet Biomasa();

        DataSet StockMadera(string tipoMad);

        bool GuardarRegistro(string ticket, string turno, string grupo,string fechapicado);

        DataSet Permisos(string usuario);

        DataSet RQPendientes(string esquemaAutorizacion);

        bool AutorizaRQ(string esquemaAutorizacion, string modfor, string codfor, string nrofor, string user);

        DataSet Produccion(string UltimaDiaMesAnt,string fechaHasta);
    }
}
