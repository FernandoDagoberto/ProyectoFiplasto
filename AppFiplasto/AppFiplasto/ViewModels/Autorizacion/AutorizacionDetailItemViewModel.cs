namespace AppFiplasto.ViewModels
{
    using AppFiplasto.Models;
    using AppFiplasto.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;

    public class AutorizacionDetailItemViewModel: RQHPendientes 
    {
        public Permisos permisos;
        public ICommand SelectItemCommand => new RelayCommand(this.SelectRegistro);

        private async void SelectRegistro()
        {
            MainViewModel.GetInstance().ConfirmarRQAutorizacion= new AutorizacionConfirmacionViewModel((RQHPendientes) this);
            await App.Navigator.PushAsync(new AutorizacionConfirmacionPage());
        }
    }
}
