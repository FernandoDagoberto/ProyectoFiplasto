using AppFiplasto.Models;
using AppFiplasto.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppFiplasto.ViewModels
{
    public class DetailProduccionViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        private ObservableCollection<ProduccionItemViewModel> produccionDetailOC;
        #endregion

        #region Propiedades
        public ObservableCollection<ProduccionItemViewModel> ProduccionDetailOC
        {
            get { return this.produccionDetailOC; }
            set { this.SetValue(ref this.produccionDetailOC, value); }
        }
        #endregion

        public DetailProduccionViewModel(Produccion produccion)
        {
            this.apiService = new ApiService();
            this.GetDetailProduccion();
        }

        private  void GetDetailProduccion()
        {
            this.ProduccionDetailOC =  new ObservableCollection<ProduccionItemViewModel>(
                               MainViewModel.GetInstance().ProduccionVM.misRegistros
                                        .Select(r => new ProduccionItemViewModel
                                        {
                                            Linea=r.Linea,
                                            Producto=r.Producto,
                                            Aberturas=r.Aberturas
                                        })
                                        .ToList());
        }

       
    }
}
