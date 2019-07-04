using AppFiplasto.Models;
using AppFiplasto.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppFiplasto.ViewModels
{
   public  class InfoViewModel : BaseViewModel
    {

        #region Atributos
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private string versionActual;
        private string mensaje;
        private Color color;
        #endregion


        #region Propiedades
        public string VersionActual
        {
            get => this.versionActual;
            set => this.SetValue(ref this.versionActual, value);
        }

        public string Mensaje
        {
            get => this.mensaje;
            set => this.SetValue(ref this.mensaje, value);
        }

        public Color Color
        {
            get => this.color;
            set => this.SetValue(ref this.color, value);
        }

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
        #endregion


        #region Constructor
        public InfoViewModel()
        {
            apiService = new ApiService();
            this.IsEnabled = true;
            this.GetVersion();
        }

        #endregion


        #region Methods
        private async void GetVersion()
        {
            VersionTracking.Track();
            int Compilacion = int.Parse(VersionTracking.CurrentBuild);
            int Version = int.Parse(VersionTracking.CurrentVersion);
            VersionActual = $"{Compilacion}.{Version}";

            var response = await apiService.ControlVersion(Compilacion, Version);

            if (!response.IsSuccess)
            {
                Mensaje = response.Message;
                Color = Color.Black;
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            Mensaje = response.Message;
            Color = Color.Blue;

        }

        public ICommand ClickCommand => new Command<string>((url) =>
        {
            if (Color == Color.Black)
            {

            }
            else
            {
                Device.OpenUri(new Uri(url));
            }

            
        });

        #endregion

    }
}
