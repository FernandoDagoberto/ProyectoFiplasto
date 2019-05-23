﻿using AppFiplasto.Models;
using AppFiplasto.Services;
using AppFiplasto.Views;
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
    public class AutorizacionDetailViewModel : BaseViewModel
    {

        #region Atributos
        private ApiService apiService;

        public List<RQHPendientes> misRequerimientos;

        private ObservableCollection<AutorizacionDetailItemViewModel> autorizacionDetalleOC;

        private bool isRefreshing;

        #endregion

        #region Propiedades
        public ObservableCollection<AutorizacionDetailItemViewModel> AutorizacionDetalleOC
        {
            get { return this.autorizacionDetalleOC; }
            set { this.SetValue(ref this.autorizacionDetalleOC, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        //public ICommand RefreshCommand => new RelayCommand(this.LoadRequerimientos(permiso));
       
        #endregion

        #region Constructor
        public AutorizacionDetailViewModel(string permiso)
        {
            this.apiService = new ApiService();
            this.LoadRequerimientos(permiso);
        }

        #endregion

        #region Methods
        private async void LoadRequerimientos(string permiso)
        {
            this.IsRefreshing = true;
            var response = await apiService.RqPendientesJSON<RQHPendientes>(permiso);

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "ok");
                return;
                
            };

            this.misRequerimientos = (List<RQHPendientes>)response.Result;
            this.RefreshList();
        }

        private void RefreshList()
        {
            this.AutorizacionDetalleOC = new ObservableCollection<AutorizacionDetailItemViewModel>(
                   this.misRequerimientos.Select(r => new AutorizacionDetailItemViewModel
                   {
                       CORMVH_MODFOR=r.CORMVH_MODFOR,
                       CORMVH_CODFOR = r.CORMVH_CODFOR,
                       CORMVH_FCHMOV = r.CORMVH_FCHMOV,
                       CORMVH_NROFOR = r.CORMVH_NROFOR,
                       CORMVH_TEXTOS = r.CORMVH_TEXTOS,
                       CORMVH_COFLIS=r.CORMVH_COFLIS,
                       SINIMP = r.SINIMP


                   })
                   .OrderBy(p => p.CORMVH_NROFOR)
                   .ToList());
        }

        public void DeleteRQInList(string codfor,int nrofor)
        {
            var previusProduct = this.misRequerimientos
                .Where(p => p.CORMVH_CODFOR == codfor && p.CORMVH_NROFOR == nrofor).FirstOrDefault();
            if(previusProduct!=null)
            {
                this.misRequerimientos.Remove(previusProduct);
            }
            this.RefreshList();
        }
       

    }



    #endregion
}