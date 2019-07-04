using AppFiplasto.ViewModels;
using AppFiplasto.Views;
using AppFiplasto.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppFiplasto
{
    public partial class App : Application
    {
        #region Propiedades

        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        #endregion

        public App()
        {
            InitializeComponent();
            if (Settings.IsRemember)
            {
                var mainViewModel = MainViewModel.GetInstance();
              /* mainViewModel.UsuarioLogueado = Settings.Usuario;
                mainViewModel.DescarteBiomasaVM = new BioPendienteViewModel();
                mainViewModel.CargadosBiomasaVM = new BioCargadosViewModel();
                mainViewModel.InformeBiomasaVM = new BioInformeViewModel();
                mainViewModel.StockVM = new StockMadViewModel();
                mainViewModel.BioStockMadVM = new BioStockMadViewModel();
                mainViewModel.ProduccionVM = new ProduccionViewModel();
               mainViewModel.AutorizacionesVM = new AutorizacionViewModel();*/
                mainViewModel.InfoVM = new InfoViewModel();
               
                this.MainPage = new MasterPage();
                return;
            }

            MainViewModel.GetInstance().Login = new LoginViewModel();
            this.MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        

    }
}
