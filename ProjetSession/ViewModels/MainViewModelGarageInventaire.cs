using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetSession.Models;
using ProjetSession.Data;
using System.Collections.ObjectModel;
using System.Threading;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation.Metadata;
using Microsoft.UI.Xaml;

namespace ProjetSession.ViewModels
{
    /// <summary>
    /// MainViewModel de la page GarageInventaire
    /// Contient les données des véhicules terrestres dans le garage
    /// </summary>
    public class MainViewModelGarageInventaire : ViewModelBase
    {
        private readonly IVehiculeTerrestreDataProvider _vehiculeTerrestreDataProvider;
        private readonly IEntretiensDataProvider _entretienDataProvider;

        public ObservableCollection<VehiculeTerrestreViewModel> VehiculeTerrestres { get; }
        public ObservableCollection<VehiculeTerrestreViewModel> VehiculesValides { get; set; }
        public ObservableCollection<string> ListeModeleVehicules { get; set; }
        public ObservableCollection<string> ListeMarques { get; set; }



        private VehiculeTerrestreViewModel _vehiculeSelectionnee;
        public VehiculeTerrestreViewModel VehiculeSelectionnee
        {
            get => _vehiculeSelectionnee;
            set
            {
                if (_vehiculeSelectionnee != value)
                {
                    _vehiculeSelectionnee = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(VehiculeEstSelectionnee));
                }
            }
        }

        private VehiculeTerrestreViewModel _vehiculeRechercheSelectionnee;
        public VehiculeTerrestreViewModel VehiculeRechercheSelectionnee
        {
            get => _vehiculeRechercheSelectionnee;
            set
            {
                if (_vehiculeRechercheSelectionnee != value)
                {
                    _vehiculeRechercheSelectionnee = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool VehiculeEstSelectionnee => VehiculeSelectionnee != null;

        /// <summary>
        /// Constructeur de MainViewModelGarageInventaire qui initialise les données des véhicules terrestres
        /// </summary>
        /// <param name="vehiculeTerrestreDataProvider"></param>
        /// <param name="entretiensDataProvider"></param>
        public MainViewModelGarageInventaire(IVehiculeTerrestreDataProvider vehiculeTerrestreDataProvider, IEntretiensDataProvider entretiensDataProvider)
        {
            _vehiculeTerrestreDataProvider = vehiculeTerrestreDataProvider;
            _entretienDataProvider = entretiensDataProvider;

            VehiculeTerrestres = new ObservableCollection<VehiculeTerrestreViewModel>();

            ListeModeleVehicules = new ObservableCollection<string>();
            ListeMarques = new ObservableCollection<string>();
            VehiculesValides = new ObservableCollection<VehiculeTerrestreViewModel>();
        }

        /// <summary>
        /// Charge les véhicules terrestres dans le garage
        /// Est asynchrone pour ne pas bloquer l'interface.
        /// Permet de charger les données en arrière-plan
        /// </summary>
        /// <returns>Lorsque la tâche est terminée</returns>
        public async Task ChargerVehiculeTerrestresAsync()
        {
            VehiculeTerrestres.Clear();
            List<VehiculeTerrestre> vTerrestreData = await Task.Run(() => _vehiculeTerrestreDataProvider.GetVehiculesTerrestres());
            foreach (VehiculeTerrestre vTerrestre in vTerrestreData)
            {
                VehiculeTerrestres.Add(new VehiculeTerrestreViewModel(vTerrestre));
                VehiculesValides.Add(new VehiculeTerrestreViewModel(vTerrestre));
                ListeModeleVehicules.Add(vTerrestre.Modele);
                ListeMarques.Add(vTerrestre.Marque.ToString());
            }
        }

        /// <summary>
        /// Charge les données des véhicules terrestres
        /// </summary>
        public void ChargerDonnees()
        {
            _ = ChargerVehiculeTerrestresAsync();
        }

        /// <summary>
        /// Supprime un véhicule du garage et de la base de données (Persistance)
        /// Si aucun véhicule n'est passé en paramètre, supprime le véhicule sélectionné, par défaut
        /// </summary>
        /// <param name="vehiculeASupprimer"></param>
        public void SupprimerUnVehiculeDuGarage(VehiculeTerrestreViewModel vehiculeASupprimer=null)
        {
            VehiculeTerrestreViewModel vehicule = vehiculeASupprimer ?? VehiculeSelectionnee;

            if (vehicule != null)
            {
                ListeModeleVehicules.Remove(vehicule.Modele);
                VehiculesValides.Remove(vehicule);
                _vehiculeTerrestreDataProvider.RetirerVehiculeTerrestre(vehicule.VehiculeTerrestre);
                VehiculeTerrestres.Remove(vehicule);
                VehiculeSelectionnee = null;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ListeModeleVehicules));
                RaisePropertyChanged(nameof(VehiculeSelectionnee));
                RaisePropertyChanged(nameof(VehiculeTerrestres));
                RaisePropertyChanged(nameof(VehiculesValides));

            }
        }

        /// <summary>
        /// Met à jour l'état des véhicules terrestres si nécessaire en fonction de la date du dernier entretien
        /// Si la date du dernier entretien est plus vieille que 6 mois, met à jour l'état du véhicule
        /// Si aucun entretien n'est trouvé, ne fait rien
        /// </summary>
        public void MettreAJourEtatVehiculesSiNecessaire()
        {
            DateTimeOffset dateLimite = DateTimeOffset.Now.AddMonths(-6);

            foreach (VehiculeTerrestreViewModel vehicule in VehiculeTerrestres)
            {
                Entretiens dernierEntretien = ObtenirDernierEntretien(vehicule);

                if (dernierEntretien != null && dernierEntretien.Date < dateLimite)
                {
                    vehicule.MettreAJourEtatVehicule(dernierEntretien.TypeEntretien);
                }
            }
        }

        /// <summary>
        /// Obtient le dernier entretien le plus récent pour un véhicule terrestre donné en paramètre.
        /// Si aucun entretien n'est trouvé, retourne null
        /// </summary>
        /// <param name="vehicule"></param>
        /// <returns>L'entretien le plus récent pour le véhicule donné</returns>
        public Entretiens ObtenirDernierEntretien(VehiculeTerrestreViewModel vehicule)
        {

            List<Entretiens> entretiens = _entretienDataProvider.GetEntretiensPourVehicule(vehicule.Vin);
            Entretiens dernierEntretien = entretiens.OrderByDescending(e => e.Date).FirstOrDefault();
            return dernierEntretien ?? null;
        }
    }
}
