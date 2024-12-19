using ProjetSession.Models;
using ProjetSession.Models.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetSession.ViewModels;
using ProjetSession.Data;


namespace ProjetSession.ViewModels
{
    /// <summary>
    /// MainViewModel pour la page AjouterAuGarage de l'application. 
    /// </summary>
    public class MainViewModelAjouterAuGarage : ViewModelBase
    {
        private DBVehiculeTerrestresDataProvider _vehiculeTerrestresDataProvider;

        public int Page = 0;

        public ObservableCollection<string> Marques;
        public ObservableCollection<string> TypeVehicules;
        public ObservableCollection<string> Couleurs;
        public ObservableCollection<string> Carburants;
        public ObservableCollection<string> Transmissions;
        public ObservableCollection<string> BoiteVitesses;
        public ObservableCollection<string> TypePermis;
        public ObservableCollection<string> TypeCarrosseries;
        public ObservableCollection<string> TypeFonctionVehiculeConstruction;
        public ObservableCollection<string> TypeDomaineTravailLourd;

        public DateTimeOffset DateActuelle = DateTime.Now;
        public ObservableCollection<string> OptionOuiNon = new ObservableCollection<string> { "Oui", "Non" };

        /// <summary>
        /// Type de véhicule sélectionné par l'utilisateur dans le comboBox.
        /// </summary>
        private string _typeVehiculeSelectionee;
        public string TypeVehiculeSelectionee
        {
            get => _typeVehiculeSelectionee;
            set
            {
                if (value != _typeVehiculeSelectionee)
                {
                    _typeVehiculeSelectionee = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(TypeVehiculeSelectionee));
                    RaisePropertyChanged(nameof(VehiculePromenadeEstSelectionnee));
                    RaisePropertyChanged(nameof(VehiculeConstructionEstSelectionnee));
                    RaisePropertyChanged(nameof(VehiculeLourdEstSelectionnee));

                    NouveauVehicule = value switch
                    {
                        "Véhicule de promenade" => nouveauVehiculePromenade,
                        "Véhicule de construction" => nouveauVehiculeConstruction,
                        "Véhicule lourd" => nouveauVehiculeLourd,
                        _ => throw new NotImplementedException()
                    };
                    RaisePropertyChanged(nameof(NouveauVehicule));
                }
            }
        }

        /// Vérifie si le type de véhicule sélectionné est un véhicule de promenade, de constuction ou lourd. Et permet de cacher ou afficher une partie de la page en conséquence.
        public bool VehiculePromenadeEstSelectionnee => TypeVehiculeSelectionee == "Véhicule de promenade";
        public bool VehiculeConstructionEstSelectionnee => TypeVehiculeSelectionee == "Véhicule de construction";
        public bool VehiculeLourdEstSelectionnee => TypeVehiculeSelectionee == "Véhicule lourd";

        /// Les trois types de véhicules terrestres. Initialisés vide pour permettre la création de nouveaux véhicules.
        public VehiculePromenadeViewModel nouveauVehiculePromenade = new VehiculePromenadeViewModel(new VehiculePromenade());
        public VehiculeConstructionViewModel nouveauVehiculeConstruction = new VehiculeConstructionViewModel(new VehiculeConstruction());
        public VehiculeLourdViewModel nouveauVehiculeLourd = new VehiculeLourdViewModel(new VehiculeLourd());


        private VehiculeTerrestreViewModel _nouveauVehicule;
        public VehiculeTerrestreViewModel NouveauVehicule
        {
            get => _nouveauVehicule;
            set
            {
                _nouveauVehicule = value;
                RaisePropertyChanged(nameof(NouveauVehicule));
                RaisePropertyChanged(nameof(nouveauVehiculePromenade));
                RaisePropertyChanged(nameof(nouveauVehiculeConstruction));
                RaisePropertyChanged(nameof(nouveauVehiculeLourd));
            }
        }


        /// <summary>
        /// Constructeur du MainViewModelAjouterAuGarage qui récupère les données du DataProvider. Et initialise les listes de type d'énumération.
        /// </summary>
        /// <param name="vehiculeTerrestreDataProvider"></param>
        public MainViewModelAjouterAuGarage(DBVehiculeTerrestresDataProvider vehiculeTerrestreDataProvider)
        {
            _vehiculeTerrestresDataProvider = vehiculeTerrestreDataProvider;

            Marques = ObtenirListeTypeEnumeration(typeof(ConstructeurNom));
            Couleurs = ObtenirListeTypeEnumeration(typeof(Couleurs));
            Carburants = ObtenirListeTypeEnumeration(typeof(CarburantType));
            Transmissions = ObtenirListeTypeEnumeration(typeof(TransmissionType));
            BoiteVitesses = ObtenirListeTypeEnumeration(typeof(BoiteVitesseType));
            TypePermis = ObtenirListeTypeEnumeration(typeof(PermisType));
            TypeCarrosseries = ObtenirListeTypeEnumeration(typeof(CarrosserieVehiculePromenade));
            TypeFonctionVehiculeConstruction = ObtenirListeTypeEnumeration(typeof(ConstructionVehiculeType));
            TypeDomaineTravailLourd = ObtenirListeTypeEnumeration(typeof(DomaineType));
            TypeVehicules = new ObservableCollection<string> { "Véhicule de promenade", "Véhicule de construction", "Véhicule lourd" };
        }

        /// <summary>
        /// Permet de modifier le type de véhicule pour pouvoir entrer dans le mode modification de la page avec un véhicule à modifier dans un type valide et existant pour les Bindings 
        /// (c'est compliqué pour ce que ça en vaut vraiment, mais en réalité c'est relativement simple), 
        /// Ensuite le ViewModel est modifié pour afficher les bonnes propriétés.
        /// Si le type de véhicule n'est pas valide, une exception est lancée.
        /// </summary>
        /// <param name="vehiculeTerrestreViewModel"></param>
        /// <returns>Le véhicule qui est modifié dans un nouveau type</returns>
        /// <exception cref="NotImplementedException"></exception>
        public VehiculeTerrestre ModifierTypeVehiculeSelectionnee(VehiculeTerrestreViewModel vehiculeTerrestreViewModel)
        {

            switch (vehiculeTerrestreViewModel.VehiculeTerrestre)
            {
                case VehiculePromenade vehiculePromenade:
                    TypeVehiculeSelectionee = "Véhicule de promenade";
                    nouveauVehiculePromenade = new VehiculePromenadeViewModel(vehiculePromenade);
                    RaisePropertyChanged(nameof(nouveauVehiculePromenade));
                    NouveauVehicule = nouveauVehiculePromenade;
                    RaisePropertyChanged(nameof(NouveauVehicule.Marque));
                    RaisePropertyChanged(nameof(NouveauVehicule));
                    return vehiculePromenade;

                case VehiculeConstruction vehiculeConstruction:
                    TypeVehiculeSelectionee = "Véhicule de construction";
                    nouveauVehiculeConstruction = new VehiculeConstructionViewModel(vehiculeConstruction);
                    RaisePropertyChanged(nameof(nouveauVehiculeConstruction));
                    NouveauVehicule = nouveauVehiculeConstruction;
                    RaisePropertyChanged(nameof(NouveauVehicule));
                    RaisePropertyChanged(nameof(TypeVehiculeSelectionee));
                    return vehiculeConstruction;

                case VehiculeLourd vehiculeLourd:
                    TypeVehiculeSelectionee = "Véhicule lourd";
                    nouveauVehiculeLourd = new VehiculeLourdViewModel(vehiculeLourd);
                    RaisePropertyChanged(nameof(nouveauVehiculeLourd));
                    NouveauVehicule = nouveauVehiculeLourd;
                    RaisePropertyChanged(nameof(NouveauVehicule));
                    RaisePropertyChanged(nameof(TypeVehiculeSelectionee));
                    return vehiculeLourd;

                default:
                    throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Permet d'obtenir l'année actuelle pour les ComboBox de l'année de fabrication et d'achat. Pour la valeur maximale en validation.
        /// </summary>
        /// <returns>L'année actuelle plus 1 ans, parce que certains véhicule sont sortie récente et sont compté pour la prochaine année</returns>
        public double ObtenirAnneeMaximum()
        {
            return DateActuelle.Year + 1;
        }

        /// <summary>
        /// Permet d'obtenir une liste de type d'énumération en string pour les ComboBox de la page AjouterAuGarage.
        /// Selon le type d'énumération passé en paramètre.
        /// Si le type d'énumération n'est pas valide, une exception est lancée avec le nom du type d'énumération invalide.
        /// </summary>
        /// <param name="nomEnumeration"></param>
        /// <returns>La liste observable de string, de toutes les valeurs de l'énumération</returns>
        /// <exception cref="Exception"></exception>
        public ObservableCollection<string> ObtenirListeTypeEnumeration(Type nomEnumeration)
        {
            try
            {
                return new ObservableCollection<string>(Enum.GetValues(nomEnumeration).Cast<Enum>().Select(t => t.ToString()));
            }
            catch
            {
                throw new Exception($"Nom de type invalide : {nomEnumeration}");
            }
        }

        /// <summary>
        /// Permet d'ajouter un véhicule au garage.
        /// Met l'état initial du véhicule.
        /// L'envoi au DataProvider pour l'ajout dans la base de données. (Persistance)
        /// Crée un nouveau véhicule selon le type de véhicule sélectionné pour la suite.
        /// </summary>
        public void AjouterAuGarage()
        {
            NouveauVehicule.EtatInitalVehicule();
            _vehiculeTerrestresDataProvider.AjoutVehiculeTerrestre(NouveauVehicule.VehiculeTerrestre);
            RaisePropertyChanged(nameof(NouveauVehicule));

            nouveauVehiculePromenade = new VehiculePromenadeViewModel(new VehiculePromenade());
            nouveauVehiculeConstruction = new VehiculeConstructionViewModel(new VehiculeConstruction());
            nouveauVehiculeLourd = new VehiculeLourdViewModel(new VehiculeLourd());

            NouveauVehicule = TypeVehiculeSelectionee == "Véhicule de promenade" ? nouveauVehiculePromenade :       
                              TypeVehiculeSelectionee == "Véhicule de construction" ? nouveauVehiculeConstruction : 
                              TypeVehiculeSelectionee == "Véhicule lourd" ? nouveauVehiculeLourd : null;            
        }

        /// <summary>
        /// Permet de modifier un véhicule dans le garage. 
        /// Met à jour l'état du véhicule.
        /// L'envoi au DataProvider pour la modification dans la base de données. (Persistance)
        /// Crée un nouveau véhicule selon le type de véhicule sélectionné pour la suite.
        /// </summary>
        public void ModifierVehicule()
        {
            NouveauVehicule.MettreAJourEtatVehicule(2);
            _vehiculeTerrestresDataProvider.ModifierVehiculeTerrestre(NouveauVehicule.VehiculeTerrestre);
            RaisePropertyChanged(nameof(NouveauVehicule));

            nouveauVehiculePromenade = new VehiculePromenadeViewModel(new VehiculePromenade());
            nouveauVehiculeConstruction = new VehiculeConstructionViewModel(new VehiculeConstruction());
            nouveauVehiculeLourd = new VehiculeLourdViewModel(new VehiculeLourd());

            NouveauVehicule = TypeVehiculeSelectionee == "Véhicule de promenade" ? nouveauVehiculePromenade :
                              TypeVehiculeSelectionee == "Véhicule de construction" ? nouveauVehiculeConstruction :
                              TypeVehiculeSelectionee == "Véhicule lourd" ? nouveauVehiculeLourd : null;
        }
    }
}
