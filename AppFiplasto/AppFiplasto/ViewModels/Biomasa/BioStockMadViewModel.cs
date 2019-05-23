using AppFiplasto.Models;
using AppFiplasto.Services;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFiplasto.ViewModels
{
    public class BioStockMadViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        private ObservableCollection<StockMad> bioStockMadOC;
        public List<StockMad> misRegistros;
        public bool isRefreshing;
        #endregion

        #region Propiedades
        public ObservableCollection<StockMad> BioStockMadOC
        {
            get { return this.bioStockMadOC; }
            set { this.SetValue(ref this.bioStockMadOC, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        #endregion

        #region Constructor
        public BioStockMadViewModel()
        {
            this.apiService = new ApiService();
            this.GetStock();
        }
        #endregion

        #region Comandos
        public ICommand RefreshCommand => new RelayCommand(GetStock);
        #endregion

        #region Metodos
        private async void GetStock()
        {

            this.IsRefreshing = true;

            var response = await apiService.StockMaderaJSON<StockMad>("B");

            this.IsRefreshing = false;



            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "ok");
                return;
            };

            this.misRegistros = (List<StockMad>)response.Result;

            this.RefreshList();
        }

        private void RefreshList()
        {
            this.BioStockMadOC = new ObservableCollection<StockMad>(
                              this.misRegistros.Select(r => new StockMad
                              {
                                  Tipo = r.Tipo,
                                  Stock = r.Stock
                              })
                                      .ToList());
        }
        #endregion

    }

}


