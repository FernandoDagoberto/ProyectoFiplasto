namespace AppFiplasto.ViewModels
{
    using Xamarin.Forms;
    using AppFiplasto.Views;
    using AppFiplasto.Helpers;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using AppFiplasto.Services;

    public class MenuItemViewModel : Models.Menu
    {
        public NavigationService navigationService{ get; set; }

        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
        }


        #region Propiedad

        public ICommand SelectMenuCommand => new RelayCommand(this.SelectMenu);

        #endregion

        #region Comandos
        private  void SelectMenu()
        {
             navigationService.Navigate(PageName);
       
        }
        #endregion

    }
}
