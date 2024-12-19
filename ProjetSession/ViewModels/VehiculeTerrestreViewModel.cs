using ProjetSession.Models;
using ProjetSession.Models.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;
using Microsoft.EntityFrameworkCore;
using Humanizer;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.CodeAnalysis.CSharp;
using ProjetSession.Data;
using static ProjetSession.Models.Utilities;

namespace ProjetSession.ViewModels
{
    /// <summary>
    /// ViewModel mère de tous les véhicules terrestres.
    /// Avec les propriétés de validation et les erreurs de validation avec leurs messages.
    /// </summary>
    public class VehiculeTerrestreViewModel : ViewModelValidable
    {
        public string CheminImage => _vehiculeTerrestre.ObtenirCheminImage();

        private VehiculeTerrestre _vehiculeTerrestre;
        public VehiculeTerrestre VehiculeTerrestre
        {
            get => _vehiculeTerrestre;
            set
            {
                if (_vehiculeTerrestre != value)
                {
                    _vehiculeTerrestre = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(CheminImage));
                }
            }
        }

        /// <summary>
        /// Constructeur du ViewModel de VehiculeTerrestre.
        /// </summary>
        /// <param name="vehiculeTerrestre"></param>
        public VehiculeTerrestreViewModel(VehiculeTerrestre vehiculeTerrestre)
        {
            _vehiculeTerrestre = vehiculeTerrestre;

            _errors.Add("Vin", new ObservableCollection<ValidationResult>());
            _errors.Add("Modele", new ObservableCollection<ValidationResult>());
            _errors.Add("Couleur", new ObservableCollection<ValidationResult>());
            _errors.Add("Marque", new ObservableCollection<ValidationResult>());
            _errors.Add("Annee", new ObservableCollection<ValidationResult>());
            _errors.Add("Carburant", new ObservableCollection<ValidationResult>());
            _errors.Add("Consommation", new ObservableCollection<ValidationResult>());
            _errors.Add("BoiteVitesse", new ObservableCollection<ValidationResult>());
            _errors.Add("Transmission", new ObservableCollection<ValidationResult>());
            _errors.Add("NombreCylindres", new ObservableCollection<ValidationResult>());
            _errors.Add("ChevauxVapeur", new ObservableCollection<ValidationResult>());
            _errors.Add("PrixConstructeurAchat", new ObservableCollection<ValidationResult>());
            _errors.Add("Kilometrage", new ObservableCollection<ValidationResult>());
        }


        /// <summary>
        /// Vérifie si le VIN est unique dans la base de données. 
        /// Sinon, ajoute une erreur de validation.
        /// Elle est asynchrone pour ne pas bloquer l'interface.
        /// </summary>
        /// <param name="vin">Le vin à rechercher dans la base de données</param>
        /// <returns>Que la tâche asynchrone est terminée</returns>
        private async Task VerifierVinEstUnique(string vin)
        {
            await using DataBaseContext context = new DataBaseContext();
            if (await context.VehiculePromenades.AnyAsync(v => v.Vin == vin) ||
                await context.VehiculeConstructions.AnyAsync(v => v.Vin == vin) ||
                await context.VehiculeLourds.AnyAsync(v => v.Vin == vin))
            {
                _errors["Vin"].Add(new ValidationResult("Un véhicule avec ce VIN existe déjà."));
            }
            else
            {
                _vehiculeTerrestre.Vin = vin;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le VIN ne peut être vide.")]
        [MinLength(16, ErrorMessage = "Le VIN doit être de 17 caractères.")]
        [MaxLength(17, ErrorMessage = "Le VIN doit être de 17 caractères.")]
        [RegularExpression(@"^\d[A-Z]{10}\d{5}$", ErrorMessage = "Le VIN doit être composé de caractères alphanumériques avec le format '0AAAAAAAAAA00000'.")]
        public string Vin
        {
            get => _vehiculeTerrestre.Vin;
            set
            {
                if (_vehiculeTerrestre.Vin != value && Validate(value))
                {
                    _ = VerifierVinEstUnique(value);
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le modele ne peut être vide.")]
        [MinLength(2, ErrorMessage = "Le modele doit être au moins de 2 caractères.")]
        [DeniedValues(["Autre"], ErrorMessage = "Le modele ne peut être vide.")]
        public string Modele
        {
            get => _vehiculeTerrestre.Modele.CapitalizePremiereLettre();
            set
            {
                if (_vehiculeTerrestre.Modele != value && Validate(value))
                {
                    _vehiculeTerrestre.Modele = value; 
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(_vehiculeTerrestre.ObtenirCheminImage));
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Une couleur doit être sélectionnée.")]
        [DeniedValues(["Autre"], ErrorMessage = "Une couleur doit être sélectionnée.")]
        public string Couleur
        {
            get => _vehiculeTerrestre.Couleur.ToString();
            set
            {
                if (_vehiculeTerrestre.Couleur.ToString() != value && Validate(value))
                {
                    _vehiculeTerrestre.Couleur = (Couleurs)Enum.Parse(typeof(Couleurs), value);
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(_vehiculeTerrestre.ObtenirCheminImage));
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La marque doit être sélectionnée.")]
        [DeniedValues(["Autre"], ErrorMessage = "La marque doit être sélectionnée.")]
        public string Marque
        {
            get => _vehiculeTerrestre.Marque.ToString();
            set
            { 
                if (_vehiculeTerrestre.Marque.ToString() != value && Validate(value))
                {
                    _vehiculeTerrestre.Marque = (ConstructeurNom)Enum.Parse(typeof(ConstructeurNom), value);
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(_vehiculeTerrestre.ObtenirCheminImage));
                }
            }
        }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "L'année doit être spécifiée.")]
        [DeniedValues([1899], ErrorMessage = "L'année doit être spécifiée.")]
        public int Annee
        {
            get => _vehiculeTerrestre.Annee;
            set
            {
                if (_vehiculeTerrestre.Annee != value && Validate(value))
                {
                    _vehiculeTerrestre.Annee = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        public DateTimeOffset? DateAchat
        {
            get => _vehiculeTerrestre.DateAchat;

            set
            {
                if (_vehiculeTerrestre.DateAchat != value)
                {
                    _vehiculeTerrestre.DateAchat = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le carburant doit être sélectionné.")]
        [DeniedValues(["Autre"], ErrorMessage = "Le carburant doit être spécifiée.")]
        public string Carburant
        {
            get => _vehiculeTerrestre.Carburant.ToString();
            set
            {
                if (_vehiculeTerrestre.Carburant.ToString() != value && Validate(value))
                {
                    _vehiculeTerrestre.Carburant = (CarburantType)Enum.Parse(typeof(CarburantType), value);
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La consommation doit être spécifiée.")]
        [DeniedValues([0.0], ErrorMessage = "La consommation doit être spécifiée.")]
        public double Consommation
        {
            get => _vehiculeTerrestre.Consommation;
            set
            {
                if (_vehiculeTerrestre.Consommation != value && Validate(value))
                {
                    _vehiculeTerrestre.Consommation = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La boîte de vitesse doit être sélectionnée.")]
        [DeniedValues(["Autre"], ErrorMessage = "La boîte de vitesse doit être sélectionnée.")]
        public string BoiteVitesse
        {
            get => _vehiculeTerrestre.BoiteVitesse.ToString();
            set
            {
                if (_vehiculeTerrestre.BoiteVitesse.ToString() != value && Validate(value))
                {
                    _vehiculeTerrestre.BoiteVitesse = (BoiteVitesseType)Enum.Parse(typeof(BoiteVitesseType), value);
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La transmission doit être sélectionnée.")]
        [DeniedValues(["Autre"], ErrorMessage = "La transmission doit être sélectionnée.")]
        public string Transmission
        {
            get => _vehiculeTerrestre.Transmission.ToString();
            set
            {
                if (_vehiculeTerrestre.Transmission.ToString() != value && Validate(value))
                {
                    _vehiculeTerrestre.Transmission = (TransmissionType)Enum.Parse(typeof(TransmissionType), value);
                    RaisePropertyChanged();
                }
            }
        }

        public int NombreRoues
        {
            get => _vehiculeTerrestre.NombreRoues;
            set
            {
                if (_vehiculeTerrestre.NombreRoues != value)
                {
                    _vehiculeTerrestre.NombreRoues = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int NombrePassagers
        {
            get => _vehiculeTerrestre.NombrePassagers;
            set
            {
                if (_vehiculeTerrestre.NombrePassagers != value)
                {
                    _vehiculeTerrestre.NombrePassagers = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int NombrePortes
        {
            get => _vehiculeTerrestre.NombrePortes;
            set
            {
                if (_vehiculeTerrestre.NombrePortes != value)
                {
                    _vehiculeTerrestre.NombrePortes = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nombre de cylindres doit être spécifié.")]
        [DeniedValues([0.0], ErrorMessage = "Le nombre de cylindres doit être spécifié.")]
        public double NombreCylindres
        {
            get => _vehiculeTerrestre.NombreCylindres;
            set
            {
                if (_vehiculeTerrestre.NombreCylindres != value && Validate(value))
                {
                    _vehiculeTerrestre.NombreCylindres = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int VitesseMax
        {
            get => _vehiculeTerrestre.VitesseMax;
            set
            {
                if (_vehiculeTerrestre.VitesseMax != value)
                {
                    _vehiculeTerrestre.VitesseMax = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nombre de chevaux vapeur doit être spécifié.")]
        [DeniedValues([0], ErrorMessage = "Le nombre de chevaux vapeur doit être spécifié.")]
        public int ChevauxVapeur
        {
            get => _vehiculeTerrestre.ChevauxVapeur;
            set
            {
                if (_vehiculeTerrestre.ChevauxVapeur != value && Validate(value))
                {
                    _vehiculeTerrestre.ChevauxVapeur = value;
                    RaisePropertyChanged();
                }
            }
        }

        public double PrixConstructeurActuel
        {
            get => _vehiculeTerrestre.PrixConstructeurActuel;
            set
            {
                if (_vehiculeTerrestre.PrixConstructeurActuel != value)
                {
                    _vehiculeTerrestre.PrixConstructeurActuel = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le prix d'achat doit être spécifié.")]
        [DeniedValues([0.0], ErrorMessage = "Le prix d'achat doit être spécifié.")]
        public double PrixConstructeurAchat
        {
            get => _vehiculeTerrestre.PrixConstructeurAchat;
            set
            {
                if (_vehiculeTerrestre.PrixConstructeurAchat != value && Validate(value))
                {
                    _vehiculeTerrestre.PrixConstructeurAchat = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le kilométrage doit être spécifié.")]
        public int Kilometrage
        {
            get => _vehiculeTerrestre.Kilometrage;
            set
            {
                if (_vehiculeTerrestre.Kilometrage != value && Validate(value))
                {
                    _vehiculeTerrestre.Kilometrage = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(EtatVehicule));
                }
            }
        }

        private int _etatVehicule;
        public int EtatVehicule
        {
            get => _vehiculeTerrestre.ObtenirEtatGlobalVehicule();
            set
            {
                if (_etatVehicule != value)
                {
                    _etatVehicule = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Met à jour l'état initial du véhicule selon le kilométrage initial.
        /// </summary>
        public void EtatInitalVehicule()
        {
            int etatMoteur = 100 - (int)((Kilometrage / 1000000.0) * 100);
            int etatHuile = 100 - (int)((Kilometrage / 100000.0) * 100);
            int etatTransmission = 100 - (int)((Kilometrage / 1500000.0) * 100);
            int etatPneus = 100 - (int)((Kilometrage / 500000.0) * 100);

            _vehiculeTerrestre.EtatMoteur = etatMoteur > 0 ? etatMoteur : 0;
            _vehiculeTerrestre.EtatHuile = etatHuile > 0 ? etatHuile : 0;
            _vehiculeTerrestre.EtatTransmission = etatTransmission > 0 ? etatTransmission : 0;
            _vehiculeTerrestre.EtatPneus = etatPneus > 0 ? etatPneus : 0;

            RaisePropertyChanged(nameof(EtatVehicule));
        }

        // Au début je souhaitais faire ce genre de documentation partout, mais honnêtement, c'est absurdement long, à faire et à lire. Et surtout c# n'a pas une belle documentation je trouve.
        /// <summary>
        /// Met à jour l'état du véhicule manquant un entretien selon le type d'entretien.
        /// <list type="number">
        ///     <item> Réduit l'état du moteur de 30%</item>
        ///     <item> Réduit l'état de l'huile de 50%</item>
        ///     <item> Réduit l'état de la transmission de 30%</item>
        ///     <item> Réduit l'état des pneus de 60%</item>
        /// </list>
        /// </summary>
        /// <param name="typeEntretien">Type d'entretien à faire</param>
        public void MettreAJourEtatVehicule(int typeEntretien)
        {
            switch (typeEntretien)
            {
                case 1: // Entretien moteur
                    _vehiculeTerrestre.EtatMoteur = (int)(_vehiculeTerrestre.EtatMoteur * 0.70);
                    break;
                case 2: // Entretien huile
                    _vehiculeTerrestre.EtatHuile = (int)(_vehiculeTerrestre.EtatHuile * 0.50);
                    break;
                case 3: // Entretien transmission
                    _vehiculeTerrestre.EtatTransmission = (int)(_vehiculeTerrestre.EtatTransmission * 0.70);
                    break;
                case 4: // Entretien pneus
                    _vehiculeTerrestre.EtatPneus = (int)(_vehiculeTerrestre.EtatPneus * 0.40);
                    break;
                default:
                    break;
            }
            RaisePropertyChanged(nameof(EtatVehicule));
        }

        /// <summary>
        /// Effectue un entretien sur le véhicule selon le type d'entretien.
        /// <list type="number">
        ///     <item> Augmente l'état du moteur de 80%</item>
        ///     <item> Augmente l'état de l'huile de 60%</item>
        ///     <item> Augmente l'état de la transmission de 70%</item>
        ///     <item> Augmente l'état des pneus de 40%</item>
        /// </list>
        /// </summary>
        /// <param name="typeEntretien">Type d'entretien à faire</param>
        public void NouvelEntretienEtatVehicule(int typeEntretien)
        {
            switch (typeEntretien)
            {
                case 1: // Entretien moteur
                    _vehiculeTerrestre.EtatMoteur += (_vehiculeTerrestre.EtatMoteur * 80 / 100);
                    break;
                case 2: // Entretien huile
                    _vehiculeTerrestre.EtatHuile += (_vehiculeTerrestre.EtatHuile * 60 / 100);
                    break;
                case 3: // Entretien transmission
                    _vehiculeTerrestre.EtatTransmission += (_vehiculeTerrestre.EtatTransmission * 70 / 100);
                    break;
                case 4: // Entretien pneus
                    _vehiculeTerrestre.EtatPneus += (_vehiculeTerrestre.EtatPneus * 40 / 100);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Retire un entretien du véhicule selon le type d'entretien.
        /// Retire au véhicule selon le type d'entretien spécifié un pourcentage de son état pour revenir l'état avant l'entretien 
        /// (plus ou moins le même pourcentage).
        /// </summary>
        /// <param name="typeEntretien">Le type d'entretien à retirer</param>
        public void RetirerUnEntretienEtatVehicule(int typeEntretien)
        {
            switch (typeEntretien)
            {
                case 1:
                    _vehiculeTerrestre.EtatMoteur -= (_vehiculeTerrestre.EtatMoteur * 80 / 100);
                    break;
                case 2:
                    _vehiculeTerrestre.EtatHuile -= (_vehiculeTerrestre.EtatHuile * 60 / 100);
                    break;
                case 3:
                    _vehiculeTerrestre.EtatTransmission -= (_vehiculeTerrestre.EtatTransmission * 70 / 100);
                    break;
                case 4:
                    _vehiculeTerrestre.EtatPneus -= (_vehiculeTerrestre.EtatPneus * 40 / 100);
                    break;
                default:
                    break;
            }
            RaisePropertyChanged(nameof(EtatVehicule));
        }


        /// <summary>
        /// Retourne la date d'achat du véhicule sous forme de string avec la méthode DateToString de la classe Utilities.
        /// </summary>
        /// <returns>La date formatée</returns>
        public string DateToString()
        {
            return Utilities.DateToString(_vehiculeTerrestre.DateAchat);
        }

        /// <summary>
        /// Retourne le rapport de prix actuel par rapport au prix d'achat. Soit <, > ou =.
        /// </summary>
        /// <returns>Soit <, > ou =</returns>
        public string ObtenirRapportPrixActuelAchat()
        {
            if (PrixConstructeurActuel < PrixConstructeurAchat)
            {
                return "<";
            }
            else if (PrixConstructeurActuel > PrixConstructeurAchat)
            {
                return ">";
            }
            else
            {
                return "=";
            }
        }

        /// <summary>
        /// Retourne les erreurs de validation pour la propriété spécifiée en paramètre.
        /// Avec la méthode GetErrors de la classe ViewModelValidable.
        /// </summary>
        /// <param name="propriete"></param>
        /// <returns></returns>
        public new ObservableCollection<ValidationResult> GetErrors(string propriete)
        {
            return (ObservableCollection<ValidationResult>)base.GetErrors(propriete);
        }
    }
}
