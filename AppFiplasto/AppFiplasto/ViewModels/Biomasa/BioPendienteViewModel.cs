using AppFiplasto.Models;
using AppFiplasto.Services;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFiplasto.ViewModels
{
    public class BioPendienteViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;

        public List<BiomasaPeso> misRegistrosBiomasa;

        private ObservableCollection<BioPesoItemViewModel> pesosBiomasaOC;

        private bool isRefreshing;

        #endregion

        #region Propiedades
        public ObservableCollection<BioPesoItemViewModel> PesosBiomasaOC
        {
            get { return this.pesosBiomasaOC; }
            set { this.SetValue(ref this.pesosBiomasaOC, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ICommand RefreshCommand => new RelayCommand(LoadBiomasaPendientes);


        #endregion

        #region Constructor
        public BioPendienteViewModel()
        {
            this.apiService = new ApiService();
            this.LoadBiomasaPendientes();
        }

        #endregion

        #region Methods
        private async void LoadBiomasaPendientes()
        {
            this.IsRefreshing = true;

            var response = await apiService.BiomasaJSON<BiomasaPeso>();

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "ok");
                return;
            };

            this.misRegistrosBiomasa = (List<BiomasaPeso>)response.Result;
            this.RefreshList();
            
            
        }

        private void RefreshList()
        {
            this.PesosBiomasaOC = new ObservableCollection<BioPesoItemViewModel>(
                   this.misRegistrosBiomasa.Where(w=>w.FechaPicado==null)
                       .Select(r =>new BioPesoItemViewModel
                   {
                       Codmad=r.Codmad,
                       FechaPesada = r.FechaPesada,
                       Neto =r.Neto,                       
                       Ticket=r.Ticket,
                       Tipmad = r.Tipmad
                   })
                   .OrderBy(p=>p.FechaPesada)
                   .ToList());
        }
    }



    #endregion


}

