using AppFiplasto.Models;
using AppFiplasto.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFiplasto.ViewModels
{
    public class BioCargadosViewModel:BaseViewModel
    {

        #region Atributos
        private ApiService apiService;

        private List<BiomasaPeso> misRegistros;

        private ObservableCollection<BioPesoItemViewModel> bioCargadosOC;

        private bool isRefreshing;

        #endregion

        #region Propiedades
        public ObservableCollection<BioPesoItemViewModel> BioCargadosOC
        {
            get { return this.bioCargadosOC; }
            set { this.SetValue(ref this.bioCargadosOC, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ICommand RefreshCommand => new RelayCommand(LoadBiomasaCargados);


        #endregion


        #region Constructor
        public BioCargadosViewModel()
        {
            this.apiService = new ApiService();
            this.LoadBiomasaCargados();
        }

        #endregion


        #region Methods
        private async void LoadBiomasaCargados()
        {
            this.IsRefreshing = true;

            this.BioCargadosOC = new ObservableCollection<BioPesoItemViewModel>(
                               MainViewModel.GetInstance().DescarteBiomasaVM.misRegistrosBiomasa.Where(l => l.FechaPicado != null)
                                        .Select(r => new BioPesoItemViewModel
                                        {
                                            FechaPicado = r.FechaPicado,
                                            Codmad = r.Codmad,
                                            FechaPesada = r.FechaPesada,
                                            Neto = r.Neto,
                                            Ticket = r.Ticket

                                        })
                                        .OrderBy(P => P.Ticket)
                                        .ToList());

            this.IsRefreshing = false;
                      
           


        }

      
    }



    #endregion


}

