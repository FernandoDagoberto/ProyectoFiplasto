using AppFiplasto.Models;
using AppFiplasto.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFiplasto.ViewModels
{
    public class DetailStockMadViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        private ObservableCollection<StockMadItemViewModel> stockMaderaDetailOC;
        #endregion

        #region Propiedades
        public ObservableCollection<StockMadItemViewModel> StockMaderaDetailOC
        {
            get { return this.stockMaderaDetailOC; }
            set { this.SetValue(ref this.stockMaderaDetailOC, value); }
        }
        #endregion

        public DetailStockMadViewModel(StockMad stockMad)
        {
            this.apiService = new ApiService();
            this.GetStock(stockMad);
        }

        private  void GetStock(StockMad stockMad)
        {
            this.StockMaderaDetailOC =  new ObservableCollection<StockMadItemViewModel>(
                               MainViewModel.GetInstance().StockVM.misRegistros.Where(l => l.Tipo == stockMad.Tipo)
                                        .Select(r => new StockMadItemViewModel
                                        {
                                            Tipo = r.Tipo,
                                            Stock = r.Stock,
                                            Pila = r.Pila
                                        })
                                        .ToList());
        }


    }
}
