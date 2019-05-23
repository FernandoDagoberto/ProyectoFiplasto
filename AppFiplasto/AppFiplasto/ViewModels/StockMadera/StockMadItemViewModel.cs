using AppFiplasto.Models;
using AppFiplasto.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AppFiplasto.ViewModels
{
    public class StockMadItemViewModel: StockMad
    {
        public ICommand SelectStockMadCommand => new RelayCommand(this.SelectRegistro);

               
        private async void SelectRegistro()
        {
          
            MainViewModel.GetInstance().DetailStockMadVM = new DetailStockMadViewModel((StockMad)this);

            await App.Navigator.PushAsync(new DetailStockMadPage());
        }

    }
}
