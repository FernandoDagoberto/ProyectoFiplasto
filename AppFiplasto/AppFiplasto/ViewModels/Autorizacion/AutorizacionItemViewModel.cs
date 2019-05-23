using AppFiplasto.Models;
using AppFiplasto.Views;
using GalaSoft.MvvmLight.Command;
using System;
using AppFiplasto.Helpers;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFiplasto.ViewModels
{
    public class AutorizacionItemViewModel : Permisos
    {
        public ICommand SelectAutorizacionCommand => new RelayCommand(this.SelectRegistro);

        private async void SelectRegistro()
        {
            MainViewModel.GetInstance().AutorizacionDetallesVM=new AutorizacionDetailViewModel(this.Permiso.ToString());
            Settings.Permiso = Permiso.Replace("USR", "");
            await App.Navigator.PushAsync(new AutorizacionDetailPage());
        }



       
    }


}
