using AppFiplasto.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFiplasto.Services
{
    public class ApiService
    {

        public async Task<Response> Login(string Usuario,string Password)
        {
            try
            {
                var servicio = DependencyService.Get<IValido>();
                bool resultado =  servicio.LoginWindows("Fiplasto", Usuario, Password);
                
                               
                if (!resultado)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o contraseña incorrecto",
                    };
                }
                             
                return new Response
                {
                    IsSuccess = true,
                    Message = "Login OK",
                 };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }


        public async Task<Response> BiomasaJSON<T>()
        {
            try
            {
                var servicio = DependencyService.Get<IValido>();

                var peso_biomasa = servicio.Biomasa();


                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(peso_biomasa.Tables[0]);


                if (peso_biomasa.Tables[0].Rows.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay datos",
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(JSONString);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Login OK",
                    Result = list,

                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<Response> StockMaderaJSON<T>(string tipoMad)
        {
            try
            {
                var servicio = DependencyService.Get<IValido>();

                var stock_madera = servicio.StockMadera(tipoMad);


                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(stock_madera.Tables[0]);

                if (stock_madera.Tables[0].Rows.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay datos",
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(JSONString);
                              

                return new Response
                {
                    IsSuccess = true,
                    Message = "Login OK",
                    Result = list,

                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }
        
        public async Task<Response> PermisosJSON<T>(string usuario)
        {
            try
            {
                var servicio = DependencyService.Get<IValido>();

                var permisos_usuario = servicio.Permisos(usuario);


                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(permisos_usuario.Tables[0]);

                if (permisos_usuario.Tables[0].Rows.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay datos",
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(JSONString);


                return new Response
                {
                    IsSuccess = true,
                    Message = "Login OK",
                    Result = list,

                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<Response> RqPendientesJSON<T>(string permiso)
        {
            try
            {
                var servicio = DependencyService.Get<IValido>();
                var resultado = permiso.Replace("USR", "");
                var rqPendientes= servicio.RQPendientes(resultado);


                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(rqPendientes.Tables[0]);

                if (rqPendientes.Tables[0].Rows.Count == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay datos",
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(JSONString);


                return new Response
                {
                    IsSuccess = true,
                    Message = "Login OK",
                    Result = list,

                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }

        public async Task<Response> ProduccionJSON<T>(string UltimoDiaMesAnt,string FechaHasta)
        {
            try
            {
                var servicio = DependencyService.Get<IValido>();
               /* var fecha = DateTime.Now;
                fecha = new DateTime(fecha.Year, fecha.Month, fecha.Day);*/
                
                var produccion = servicio.Produccion(UltimoDiaMesAnt,FechaHasta);

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(produccion.Tables[0]);

                if (produccion.Tables[0].Rows.Count == 0 )
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No hay datos",
                    };
                }

                

                var list = JsonConvert.DeserializeObject<List<T>>(JSONString);


                return new Response
                {
                    IsSuccess = true,
                    Message = "Login OK",
                    Result = list,

                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }




    }
}
