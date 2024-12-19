using ProjetSession.Data;
using ProjetSession.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjetSession.ViewModels
{
    /// <summary>
    /// MainViewModel pour la page d'accueil de l'application (Le tableau de bord).
    /// Les données sont récupérées à partir des DataProviders.
    /// Mais ne sont pas mise à jour en temps réel.
    /// Seulement au chargement de la page, car les données sont statiques.
    /// </summary>
    public class MainViewModelAccueil : ViewModelBase
    {
        private IVehiculeTerrestreDataProvider _vehiculeTerrestreDataProvider;
        private IEntretiensDataProvider _entretiensDataProvider;
        public ObservableCollection<VehiculeTerrestreViewModel> ListeDernierVehiculeAjoute { get; set; }
        public ObservableCollection<EntretienViewModel> ListeDernierEntretienRecent { get; set; }
        public ObservableCollection<EntretienViewModel> ListeEntretienRequis { get; set; }

        public double ValeurDuGarage;
        public int NombreDeVehicules;
        public int NombreVehiculePromenade;
        public int NombreVehiculeIndustriels;

        public VehiculeTerrestreViewModel VoiturePlusPuissante;
        public VehiculeTerrestreViewModel VoiturePlusChere;

        /// <summary>
        /// Constructeur du MainViewModelAccueil qui récupère les données des DataProviders.
        /// </summary>
        /// <param name="vehiculeTerrestreDataProvider"></param>
        /// <param name="entretiensDataProvider"></param>
        public MainViewModelAccueil(IVehiculeTerrestreDataProvider vehiculeTerrestreDataProvider, IEntretiensDataProvider entretiensDataProvider)
        {
            _vehiculeTerrestreDataProvider = vehiculeTerrestreDataProvider;
            _entretiensDataProvider = entretiensDataProvider;

            ListeDernierVehiculeAjoute = new ObservableCollection<VehiculeTerrestreViewModel>();
            foreach (VehiculeTerrestre vehiculeRecent in _vehiculeTerrestreDataProvider.GetVehiculesTerrestres()
                .OrderByDescending(v => v.DateAchat)  
                .Take(5))                             
            {
                ListeDernierVehiculeAjoute.Add(new VehiculeTerrestreViewModel(vehiculeRecent));
            }


            ListeDernierEntretienRecent = new ObservableCollection<EntretienViewModel>();
            foreach (Entretiens entretienRecent in _entretiensDataProvider.GetEntretiens()
                .OrderByDescending(e => e.Date)
                .Take(5))
            {
                ListeDernierEntretienRecent.Add(new EntretienViewModel(entretienRecent));
            }

            ListeEntretienRequis = new ObservableCollection<EntretienViewModel>();
            foreach (Entretiens entretienRequis in _entretiensDataProvider.GetEntretiens()
                .OrderBy(e => e.Etat)
                .Take(5))
            {
                ListeEntretienRequis.Add(new EntretienViewModel(entretienRequis));
            }

            List<VehiculeTerrestre>  vehiculeTerrestres = _vehiculeTerrestreDataProvider.GetVehiculesTerrestres();
            ValeurDuGarage = vehiculeTerrestres.Sum(v => v.PrixConstructeurAchat);
            NombreDeVehicules = vehiculeTerrestres.Count;
            NombreVehiculePromenade = vehiculeTerrestres.OfType<VehiculePromenade>().Count();
            NombreVehiculeIndustriels = NombreDeVehicules - NombreVehiculePromenade;

            VoiturePlusPuissante = new VehiculeTerrestreViewModel(vehiculeTerrestres.OfType<VehiculePromenade>().OrderByDescending(v => v.ChevauxVapeur).FirstOrDefault());
            VoiturePlusChere = new VehiculeTerrestreViewModel(vehiculeTerrestres.OfType<VehiculePromenade>().OrderByDescending(v => v.PrixConstructeurAchat).FirstOrDefault());
        }
    }
}
