using AppFiplasto.Models;
using AppFiplasto.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AppFiplasto.ViewModels
{
    public class ProduccionItemViewModel : Produccion
    {
        public ICommand SelectProduccionCommand => new RelayCommand(this.SelectRegistro);

        private async void SelectRegistro()
        {

            MainViewModel.GetInstance().DetailProduccionVM = new DetailProduccionViewModel((Produccion)this); 
            await App.Navigator.PushAsync(new ProduccionDetailPage());
        }

    }
}
