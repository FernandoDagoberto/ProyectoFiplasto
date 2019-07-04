using AppFiplasto.Models;
using AppFiplasto.Services;
using AppFiplasto.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFiplasto.ViewModels
{
    public class AddBioPendienteViewModel : BaseViewModel
    {
        #region Atributos
        private bool isRunning;
        private bool isEnabled;
        private ApiService apiService;
        private DateTime fechaPicado;
        private Turno selectedTurno;
        private Grupo selectedGrupo;
        #endregion

        #region Properties

        public List<Turno> TurnosList { get; set; }
        public List<Grupo> GruposList { get; set; }

        public DateTime FechaPicado
        {
            get => this.fechaPicado;
            set => this.SetValue(ref this.fechaPicado, value);
        }

        public Turno SelectedTurno
        {
            get => this.selectedTurno;
            set => this.SetValue(ref this.selectedTurno, value);
        }

        public Grupo SelectedGrupo
        {
            get => this.selectedGrupo;
            set => this.SetValue(ref this.selectedGrupo, value);
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

        public BiomasaPeso BiomasaPeso { get; set; }


        public ICommand SaveCommand => new RelayCommand(this.Save);

        #endregion


        #region Constructor

        public AddBioPendienteViewModel(BiomasaPeso biomasaPeso)
        {
            this.BiomasaPeso = biomasaPeso;
            this.LoadTurnoGrupo();
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.FechaPicado = DateTime.Now;
        }

        #endregion



        #region Metodos
        private void LoadTurnoGrupo()
        {
            TurnosList = new List<Turno>();
            TurnosList.Add(new Turno() { Turnos = "06-14" });
            TurnosList.Add(new Turno() { Turnos = "14-22" });
            TurnosList.Add(new Turno() { Turnos = "22-06" });


            GruposList = new List<Grupo>();
            GruposList.Add(new Grupo() { Grupos = "A" });
            GruposList.Add(new Grupo() { Grupos = "B" });
            GruposList.Add(new Grupo() { Grupos = "C" });
            GruposList.Add(new Grupo() { Grupos = "D" });
            GruposList.Add(new Grupo() { Grupos = "E" });

        }

        private async void Save()
        {
            string FechaP = FechaPicado.ToString("yyyyMMdd");

            if (this.SelectedTurno == null || this.SelectedGrupo == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe cargar los campos turno y grupo",
                    "Aceptar"
                    );
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var servicio = DependencyService.Get<IValido>();
            bool resultado = servicio.GuardarRegistro(this.BiomasaPeso.Ticket, this.SelectedTurno.Turnos.ToString(), this.SelectedGrupo.Grupos.ToString(), FechaP);

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

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.DescarteBiomasaVM = new BioPendienteViewModel();
           Application.Current.MainPage = new MasterPage();



        }
        #endregion




    }
}
