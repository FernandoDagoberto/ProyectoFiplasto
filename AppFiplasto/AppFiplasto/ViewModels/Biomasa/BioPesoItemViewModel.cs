namespace AppFiplasto.ViewModels
{
    using AppFiplasto.Models;
    using AppFiplasto.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;

    public class BioPesoItemViewModel:BiomasaPeso
    {
        public ICommand SelectBiomasaPesoCommand => new RelayCommand(this.SelectRegistro);

        private async void SelectRegistro()
        {
            MainViewModel.GetInstance().AddRegistroBiomasa = new AddBioPendienteViewModel((BiomasaPeso)this);
            await App.Navigator.PushAsync(new AddBioPendientePage());
        }
    }
}
