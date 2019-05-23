using AppFiplasto.Models;
using AppFiplasto.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFiplasto.ViewModels
{
    public class ProduccionViewModel : BaseViewModel

    {
        #region Atributos
        private ApiService apiService;
        private ObservableCollection<Produccion> prodMensualOC;
        private ObservableCollection<Produccion> prodDiariaOC;
        public List<Produccion> misRegistros;
        public bool isRefreshing;
        private DateTime fechaDesde;
        private string fchDesde, fchHasta;
        #endregion

        #region Propiedades
        public ObservableCollection<Produccion> ProdMensualOC
        {
            get { return this.prodMensualOC; }
            set { this.SetValue(ref this.prodMensualOC, value); }
        }

        public ObservableCollection<Produccion> ProdDiariaOC
        {
            get { return this.prodDiariaOC; }
            set { this.SetValue(ref this.prodDiariaOC, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public DateTime FechaDesde
        {
            get => this.fechaDesde;
            set => this.SetValue(ref this.fechaDesde, value);
        }

        #endregion

        #region Constructor
        public ProduccionViewModel()
        {
            this.apiService = new ApiService();
            this.FechaDesde = DateTime.Now.Date;
            this.GetProduccion();
            
        }

        private void GetProduccion()
        {
            this.GetProduccion("M");
            this.GetProduccion("D"); 
        }
        #endregion

        #region Comandos
        public ICommand RefreshCommand => new RelayCommand(GetProduccion);
        #endregion

        #region Metodos
        private async void GetProduccion(string Opcion)
        {
            this.IsRefreshing = true;
            

            if (Opcion == "M")
            {

                DateTime UltimoDiaMesAnterior = new DateTime(FechaDesde.Year, FechaDesde.Month, 1);
                UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1);
                UltimoDiaMesAnterior = new DateTime(UltimoDiaMesAnterior.Year, UltimoDiaMesAnterior.Month, UltimoDiaMesAnterior.Day, 22, 00, 00);
                fchDesde = UltimoDiaMesAnterior.ToString("dd/MM/yyyy 22:00:00");
                fchHasta = FechaDesde.ToString("dd/MM/yyyy 21:59:59");
            }
            else {
                DateTime fDesde = FechaDesde.AddDays(-1);
                fchDesde = fDesde.ToString("dd/MM/yyyy 22:00:00");
                fchHasta = FechaDesde.ToString("dd/MM/yyyy 21:59:59");
              }

            var response = await apiService.ProduccionJSON<Produccion>(fchDesde, fchHasta);

            this.IsRefreshing = false;


            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "ok");
                return;
            };

            this.misRegistros = (List<Produccion>)response.Result;
            this.RefreshList(Opcion);
        }

        private void RefreshList(string Opcion)
        {
            if (Opcion == "M")
            {
                this.ProdMensualOC = new ObservableCollection<Produccion>(
                                  this.misRegistros.Select(r => new Produccion
                                  {
                                      Linea = r.Linea,
                                      Producto = r.Producto,
                                      Aberturas = r.Aberturas,
                                  })
                                         .OrderBy(o => o.Linea)
                                          .ToList());
            }
            else
            {
                this.ProdDiariaOC = new ObservableCollection<Produccion>(
                                 this.misRegistros.Select(r => new Produccion
                                         {                                    
                                             Linea = r.Linea,
                                             Producto = r.Producto,
                                             Aberturas = r.Aberturas,
                                        })
                                         .OrderBy(o => o.Linea)
                                         .ToList());
            }
        }
        #endregion
    }
}
