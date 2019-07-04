using AppFiplasto.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AppFiplasto.ViewModels
{
    public class BioInformeViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        private DateTime fechaDesde;
        private DateTime fechaHasta;


        private ObservableCollection<BioPesoItemViewModel> informeBiomasaOC;

        private bool isRefreshing;

        #endregion

        #region Propiedades
        public ObservableCollection<BioPesoItemViewModel> InformeBiomasaOC
        {
            get { return this.informeBiomasaOC; }
            set { this.SetValue(ref this.informeBiomasaOC, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public DateTime FechaDesde
        {
            get => this.fechaDesde;
            set => this.SetValue(ref this.fechaDesde, value);
        }

        public DateTime FechaHasta
        {
            get => this.fechaHasta;
            set => this.SetValue(ref this.fechaHasta, value);
        }

        public ICommand RefreshCommand => new RelayCommand(LoadInforme);

        public ICommand FindCommand => new RelayCommand(LoadInforme);


        #endregion


        #region Constructor
        public BioInformeViewModel()
        {
            this.apiService = new ApiService();
            this.FechaDesde = DateTime.Now.AddDays(-5);
            this.FechaHasta = DateTime.Now;
            this.LoadInforme();
        }

        #endregion


        #region Methods
        private async void LoadInforme()
        {
            this.IsRefreshing = true;

            this.InformeBiomasaOC = new ObservableCollection<BioPesoItemViewModel>(
                               MainViewModel.GetInstance().DescarteBiomasaVM.misRegistrosBiomasa.Where(l=>l.FechaPesada>=this.FechaDesde & l.FechaPesada<=this.FechaHasta)
                               .GroupBy(l => l.Tipmad)
                               .Select(r => new BioPesoItemViewModel
                               {
                                   Tipmad = r.Key,
                                   Neto = r.Sum(s => s.Neto),
                                   
                               })
                               .ToList());
                       
            this.IsRefreshing = false;

        }
        #endregion

    }
}
