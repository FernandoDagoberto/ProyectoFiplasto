namespace AppFiplasto.ViewModels
{
    using AppFiplasto.Models;
    using AppFiplasto.Services;
    using GalaSoft.MvvmLight.Command;
    using AppFiplasto.Helpers;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AutorizacionConfirmacionViewModel:BaseViewModel
    {
        #region Atributos
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;
        #endregion


        #region Properties
        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public RQHPendientes RQHPendientes { get; set; }
        


        public ICommand AutorizaCommand => new RelayCommand(this.Autoriza);
        //public ICommand NoAutorizaCommand => new RelayCommand(this.NoAutoriza);

        #endregion


        #region Constructor

        public AutorizacionConfirmacionViewModel(RQHPendientes datosRQ)
        {   
            this.RQHPendientes = datosRQ;
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }

        #endregion

        #region Métodos
        private async void Autoriza()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            var servicio = DependencyService.Get<IValido>();
            bool resultado = servicio.AutorizaRQ(Settings.Permiso,this.RQHPendientes.CORMVH_MODFOR, this.RQHPendientes.CORMVH_CODFOR, this.RQHPendientes.CORMVH_NROFOR.ToString(), Settings.Usuario);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!resultado)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "No se pudo guardar el registro",
                    "Aceptar"
                    );
                return;
            }

            await Application.Current.MainPage.DisplayAlert(
                  "Información",
                  "Movimiento registrado",
                  "Aceptar"
                  );

            

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.AutorizacionDetallesVM.DeleteRQInList(this.RQHPendientes.CORMVH_CODFOR, this.RQHPendientes.CORMVH_NROFOR);
            await App.Navigator.PopAsync();

          
            //Application.Current.MainPage = new MasterPage();
            


        }
#endregion

    }
}
