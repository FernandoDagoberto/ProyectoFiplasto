namespace AppFiplasto.ViewModels
{
    using AppFiplasto.Models;
    using AppFiplasto.Services;
    using GalaSoft.MvvmLight.Command;
    using AppFiplasto.Helpers;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    using System.Linq;

    public class AutorizacionViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;

        public List<Permisos> misPermisos;

        private ObservableCollection<AutorizacionItemViewModel> autorizacionesOC;

        private bool isRefreshing;

        #endregion

        #region Propiedades
        public ObservableCollection<AutorizacionItemViewModel> AutorizacionesOC
        {
            get { return this.autorizacionesOC; }
            set { this.SetValue(ref this.autorizacionesOC, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ICommand RefreshCommand => new RelayCommand(LoadPermisos);


        #endregion

        #region Constructor
        public AutorizacionViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPermisos();
        }

        #endregion

        #region Methods
        private async void LoadPermisos()
        {
            this.IsRefreshing = true;

            var response = await apiService.PermisosJSON<Permisos>(Settings.Usuario);

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "ok");
                return;
            };

            this.misPermisos = (List<Permisos>)response.Result;
            this.RefreshList();
        }

        private void RefreshList()
        {
            this.AutorizacionesOC = new ObservableCollection<AutorizacionItemViewModel>(
                   this.misPermisos.Select(r => new AutorizacionItemViewModel
                   {
                       Permiso = r.Permiso,
                       Descripcion = r.Descripcion
                   })
                   .OrderBy(p => p.Descripcion)
                   .ToList());
        }
    }



    #endregion

}

