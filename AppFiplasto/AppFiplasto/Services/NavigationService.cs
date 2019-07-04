using Xamarin.Forms;
using AppFiplasto.Views;
using AppFiplasto.Helpers;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using AppFiplasto.ViewModels;

namespace AppFiplasto.Services
{
    public class NavigationService
    {

        public async void Navigate(string pageName)
        {
            App.Master.IsPresented = false;
            var mainViewModel = MainViewModel.GetInstance();

            switch (pageName)
            {
                case "InfoPage":
                    await App.Navigator.PushAsync(new InfoPage());
                    break;
                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                case "BioTabPage":
                    await App.Navigator.PushAsync(new BioTabPage());
                    break;
                case "StockTabPage":
                    await App.Navigator.PushAsync(new StockTabPage());
                    break;
                case "AutorizacionPage":
                    await App.Navigator.PushAsync(new AutorizacionPage());
                    break;
                case "ProduccionPage":
                    await App.Navigator.PushAsync(new ProduccionPage());
                    break;
                default:
                    Settings.IsRemember = false;
                    Settings.Usuario = string.Empty;

                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }

        }

    }
}
