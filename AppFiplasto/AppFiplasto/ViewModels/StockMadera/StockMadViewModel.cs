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
    public class StockMadViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        private ObservableCollection<StockMadItemViewModel> stockMaderaOC;
        public List<StockMad> misRegistros;
        public bool isRefreshing;
        #endregion

        #region Propiedades
        public ObservableCollection<StockMadItemViewModel> StockMaderaOC
        {
            get { return this.stockMaderaOC; }
            set { this.SetValue(ref this.stockMaderaOC, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        #endregion

        #region Constructor
        public StockMadViewModel()
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

            var response = await apiService.StockMaderaJSON<StockMad>("M");

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
            this.StockMaderaOC = new ObservableCollection<StockMadItemViewModel>(
                              this.misRegistros.GroupBy(l => l.Tipo)
                                      .Select(r => new StockMadItemViewModel
                                      {
                                          Tipo = r.Key,
                                          Stock = r.Sum(s => s.Stock),
                                      })
                                      .ToList());
        }
        #endregion

    }

}


