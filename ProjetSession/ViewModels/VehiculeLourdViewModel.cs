using ProjetSession.Models.Types;
using ProjetSession.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.ViewModels
{
    /// <summary>
    /// ViewModel pour les véhicules Lourd.
    /// Avec les propriétés de validation et les erreurs de validation avec leurs messages.
    /// Qui hérite du ViewModel pour les véhicules terrestres.
    /// </summary>
    public class VehiculeLourdViewModel : VehiculeTerrestreViewModel
    {
        private VehiculeLourd _vehiculeLourd;

        /// <summary>
        /// Constructeur du ViewModel pour les véhicules Lourd.
        /// </summary>
        /// <param name="vehiculeLourd"></param>
        public VehiculeLourdViewModel(VehiculeLourd vehiculeLourd) : base(vehiculeLourd)
        {
            _vehiculeLourd = vehiculeLourd;

            _errors.Add("Domaine", new ObservableCollection<ValidationResult>());
            _errors.Add("PoidsMax", new ObservableCollection<ValidationResult>());
            _errors.Add("ChargementMax", new ObservableCollection<ValidationResult>());
            _errors.Add("Hauteur", new ObservableCollection<ValidationResult>());
            _errors.Add("VitesseMaxAutorisee", new ObservableCollection<ValidationResult>());
            _errors.Add("PermisRequis", new ObservableCollection<ValidationResult>());
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le domaine ne peut être vide.")]
        [DeniedValues(["Autre"], ErrorMessage = "Le domaine ne peut être vide.")]
        public string Domaine
        {
            get => _vehiculeLourd.Domaine.ToString();
            set
            {
                if (value != _vehiculeLourd.Domaine.ToString() && Validate(value))
                {
                    _vehiculeLourd.Domaine = (DomaineType)Enum.Parse(typeof(DomaineType), value);
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le poids maximum doit être mentionné.")]
        [DeniedValues([0.0], ErrorMessage = "Le poids maximum doit être mentionné.")]
        public double PoidsMax
        {
            get => _vehiculeLourd.PoidsMax;
            set
            {
                if (value != _vehiculeLourd.PoidsMax && Validate(value))
                {
                    _vehiculeLourd.PoidsMax = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le chargement maximum doit être mentionné.")]
        [DeniedValues([0.0], ErrorMessage = "Le chargement maximum doit être mentionné.")]
        public double ChargementMax
        {
            get => _vehiculeLourd.ChargementMax;
            set
            {
                if (value != _vehiculeLourd.ChargementMax && Validate(value))
                {
                    _vehiculeLourd.ChargementMax = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La hauteur doit être mentionné.")]
        [DeniedValues([0.0], ErrorMessage = "La hauteur doit être mentionné.")]
        public double Hauteur
        {
            get => _vehiculeLourd.Hauteur;
            set
            {
                if (value != _vehiculeLourd.Hauteur && Validate(value))
                {
                    _vehiculeLourd.Hauteur = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La vitesse maximale autorisée doit être mentionné.")]
        [DeniedValues([0], ErrorMessage = "La vitesse maximale autorisée doit être mentionné.")]
        public int VitesseMaxAutorisee
        {
            get => _vehiculeLourd.VitesseMaxAutorisee;
            set
            {
                if (value != _vehiculeLourd.VitesseMaxAutorisee && Validate(value))
                {
                    _vehiculeLourd.VitesseMaxAutorisee = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le permis requis doit être mentionné.")]
        [DeniedValues(["Autre"], ErrorMessage = "Le permis requis doit être mentionné.")]
        public String PermisRequis
        {
            get => _vehiculeLourd.PermisRequis.ToString();
            set
            {
                if (value != _vehiculeLourd.PermisRequis.ToString() && Validate(value))
                {
                    _vehiculeLourd.PermisRequis = (PermisType)Enum.Parse(typeof(PermisType), value);
                    RaisePropertyChanged();
                }
            }
        }
    }
}
