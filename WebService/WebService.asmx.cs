using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace WebService
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        

        [WebMethod]
        public DataSet ListaStockMadera(string Tipo)
        {
            SqlConnection conn = new SqlConnection(new DBConnection().ConnectionString);
            Result result = new Result();
             string sql;

            try
            {
                if (Tipo == "M")
                {

                     sql = "SELECT stpila_pila as Pila, pila_tipo as Tipo," +
                        "CONVERT(FLOAT, stpila_stock) / 1000 as Stock " +
                        "FROM stpila inner join pila on pila_id = stpila_pila " +
                        "WHERE stpila_stock>0 ";
                }
                else
                {
                     sql = "SELECT Pila, Tipo,CONVERT(FLOAT, BioStock.stock) / 1000 as Stock FROM BIOSTOCK " +
                                 "inner join biopila on biopila.ID = pila where BioStock.stock> 0 ";
                }


                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                // Fill The DataSet With the Contents of the Stock Table
                DataSet ds = new DataSet();
                da.Fill(ds, "stock");


                return ds;
            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return null;
        }

        [WebMethod]
        public DataSet RemitosBiomasa()
        {
            SqlConnection conn = new SqlConnection(new DBConnection().ConnectionString);
            conn.Open();


            Result result = new Result();

            try
            {

                string sql = "SELECT * From BIODESCARTE";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                // Fill The DataSet With the Contents of the Stock Table
                DataSet ds = new DataSet();
                da.Fill(ds, "Reporte");


                return ds;
            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return null;
        }

        [WebMethod]
        public bool GrabarTicketBio(string ticket, string turno, string grupo, string fechapicado)
        {
            SqlConnection conn = new SqlConnection(new DBConnection().ConnectionString);
            Result result = new Result();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Update BIODESCARTE set turno='" + turno + "'," +
                    " grupo='" + grupo + "', " +
                    " fechapicado='" + fechapicado + "' " +
                    " where ticket='" + ticket + "' ", conn);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        [WebMethod]
        public DataSet Permisos(string usuario)
        {
            SqlConnection conn = new SqlConnection(new DBConnection().ConnectionCwSgCore);
            conn.Open();


            Result result = new Result();
            DataSet ds = new DataSet();

            try
            {

                switch (usuario)
                {
                    case "mpereyra":
                        usuario = "mpereira";
                        break;
                                 
                };

                string sql = "SELECT cwOMUR_UID as Usuario,cwOMUR_ObjectName as Permiso,cwOMUR_ObjectDesc as Descripcion,0 as Pendientes FROM cwOMUserRights" +
                        " where cwOMUR_UID = '" + usuario + "'and cwOMUR_ObjectDesc like 'autori%' and cwOMUR_AccessLevel = 8 and cwOMUR_CompanyName = 'FIPLASTO'";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                // Fill The DataSet With the Contents of the Stock Table
            
                
                da.Fill(ds, "Reporte");
              
                foreach (DataRow row in ds.Tables["Reporte"].Rows)
                {

                  DataSet cantidad=  RQPendientes(row["Permiso"].ToString().Replace("USR", ""));

                    if (cantidad  is null)
                    {
                        
                    }
                    else
                    {
                        var numero = cantidad.Tables[0].Rows.Count;
                        if (numero>0)
                        {
                            row["Pendientes"] = numero;

                        }
                        else
                        {
                            row.Delete();
                        }
                    }
                }

                ds.AcceptChanges();
                return ds;
            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return null;
        }


        [WebMethod]
        public DataSet RQPendientes(string esquemaAutorizacion)
        {
            SqlConnection conn = new SqlConnection(new DBConnection().ConnectionFiplasto);
            conn.Open();

            Result result = new Result();

            try
            {

                string sql1 = "SELECT GRTDAH_RECCOM From GRTDAH where GRTDAH_CODEST='" + esquemaAutorizacion + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(sql1, conn);
                // Fill The DataSet With the Contents of the Stock Table
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "Reporte");

                DataTable dt1 = new DataTable();
                dt1 = ds1.Tables[0];

                DataRow row = dt1.Rows[0];

                string consulta = row["GRTDAH_RECCOM"].ToString();



                var resultado = consulta.Replace("@@GetDate", "'20190101'")
                                        .Replace("CORMVH, GRCFOR, GRTCOF, GRTIVA, PVTCOM, PVMPRH ", "CORMVH, GRCFOR, GRTCOF, GRTIVA, PVTCOM, PVMPRH, COTCIH ")
                                         .Replace("AND GRTIVA_CNDIVA = PVMPRH_CNDIVA", " AND GRTIVA_CNDIVA = PVMPRH_CNDIVA AND CORMVH_FCHMOV >={ d '2018-05-23'}AND CORMVH_FCHMOV <={ d '2040-05-23'}AND COTCIH_CIRCOM = CORMVH_CIRCOM AND COTCIH_CIRAPL = CORMVH_CIRAPL" +
                    " AND 1 = Case When COTCIH_CNTCER = 'S'Then Case When Exists(Select 1 as Existe from CORMVI B, CORMVH C, COTCIH D Where B.CORMVI_MODAPL = CORMVH.CORMVH_MODFOR" +
" And B.CORMVI_CODAPL = CORMVH.CORMVH_CODFOR And B.CORMVI_NROAPL = CORMVH.CORMVH_NROFOR And(B.CORMVI_CODFOR <> B.CORMVI_CODAPL Or B.CORMVI_NROFOR <> B.CORMVI_NROAPL)" +
" And B.CORMVI_MODFOR = C.CORMVH_MODFOR And B.CORMVI_CODFOR = C.CORMVH_CODFOR And B.CORMVI_NROFOR = C.CORMVH_NROFOR And D.COTCIH_CIRCOM = C.CORMVH_CIRCOM" +
" And D.COTCIH_CIRAPL = C.CORMVH_CIRAPL And D.COTCIH_ANULAC = 'S' And(SELECT SUM(CORMVI_CANTID) FROM CORMVI  WHERE CORMVI_MODAPL = CORMVH.CORMVH_MODFOR" +
" AND CORMVI_CODAPL = CORMVH.CORMVH_CODFOR  AND CORMVI_NROAPL = CORMVH.CORMVH_NROFOR) = 0 ) Then 0 Else 1 End Else    Case When(SELECT SUM(CORMVI_CANTID)" +
" FROM CORMVI WHERE CORMVI_MODAPL = CORMVH_MODFOR AND CORMVI_CODAPL = CORMVH_CODFOR  AND CORMVI_NROAPL = CORMVH_NROFOR) > 0" +
" Then 1  Else 0  End End  Order By CORMVH_MODFOR, CORMVH_CODFOR, CORMVH_NROFOR ");



                SqlDataAdapter da2 = new SqlDataAdapter(resultado, conn);
                // Fill The DataSet With the Contents of the Stock Table
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "Reporte");



                return ds2;
            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return null;
        }

        [WebMethod]
        public bool AutorizaRQ(string esquemaAutorizacion, string modfor, string codfor, string nrofor, string user)
        {
            SqlConnection conn = new SqlConnection(new DBConnection().ConnectionFiplasto);
            conn.Open();

            Result result = new Result();

            try
            {

                string sql = "SELECT GRTDAH_ACTCOM From GRTDAH where GRTDAH_CODEST='" + esquemaAutorizacion + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                // Fill The DataSet With the Contents of the Stock Table
                DataSet ds = new DataSet();
                da.Fill(ds, "Reporte");

                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                DataRow row = dt.Rows[0];

                string consulta = row["GRTDAH_ACTCOM"].ToString();



                var resultado = consulta.Replace("@@GetDate", "'20190101'")
                                      .Replace("&USR", "'" + user + "'")

                                        .Replace(", @@CAMPOSREM", "")
                                         .Replace("AND GRTIVA_CNDIVA = PVMPRH_CNDIVA", " AND GRTIVA_CNDIVA = PVMPRH_CNDIVA AND" +
                                         " CORMVH_MODFOR='" + modfor + "' AND CORMVH_CODFOR='" + codfor + "' AND CORMVH_NROFOR=" + nrofor + " ");


                SqlCommand cmd = new SqlCommand(resultado, conn);


                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
                                    
        [WebMethod]
        public DataSet InformeProduccion(string UltimoDiaMesAnterior,string FechaHasta)
        {

          SqlConnection conn = new SqlConnection(new DBConnection().ConnectionString);
            conn.Open();
            Result result = new Result();

            try
            {
                string sql = "SELECT linea ,sum(aberturas * 6.7 * factor) as aberturas,producto " +
                            "FROM prensadas WHERE " +
                            "hora>='" + UltimoDiaMesAnterior+ "' and " +
                            "hora<='" + FechaHasta + "' and " +
                            "producto is not null " +
                            "group by linea,producto " ;
                              SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                // Fill The DataSet With the Contents of the Stock Table
                DataSet ds = new DataSet();
                da.Fill(ds, "Reporte");


                return ds;

            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
                
            }
            finally
            {
                conn.Close();

            }
            return null;
        }

        [WebMethod]
        public bool ControlaVersion(int Compilacion,int Version)
        {
            Result result = new Result();
            int ultimaCompilacion = 1;
            int ultimaVersion= 2;
            try
            {
                if (ultimaCompilacion>Compilacion || ultimaVersion > Version)
                {
                   return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
            }
            finally
            {
                
            }
            return false;
        }
    }
}
