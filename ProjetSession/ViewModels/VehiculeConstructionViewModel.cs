using ProjetSession.Models;
using ProjetSession.Models.Types;
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
    /// ViewModel pour les véhicules de construction.
    /// Avec les propriétés de validation et les erreurs de validation avec leurs messages.
    /// Qui hérite du ViewModel pour les véhicules terrestres.
    /// </summary>
    public class VehiculeConstructionViewModel : VehiculeTerrestreViewModel
    {
        private VehiculeConstruction _vehiculeConstruction;

        /// <summary>
        /// Constructeur du ViewModel pour les véhicules de construction.
        /// </summary>
        /// <param name="vehiculeConstruction"></param>
        public VehiculeConstructionViewModel(VehiculeConstruction vehiculeConstruction) : base(vehiculeConstruction)
        {
            _vehiculeConstruction = vehiculeConstruction;

            _errors.Add("EstHomologueRoute", new ObservableCollection<ValidationResult>());
            _errors.Add("PermisSpecial", new ObservableCollection<ValidationResult>());
            _errors.Add("TypeVehicule", new ObservableCollection<ValidationResult>());
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'option homologué pour la route ne peut être vide.")]
        [DeniedValues([null], ErrorMessage = "L'option homologué pour la route ne peut être vide.")]
        public bool? EstHomologueRoute
        {
            get => _vehiculeConstruction.EstHomologueRoute;
            set
            {
                if (value != _vehiculeConstruction.EstHomologueRoute && Validate(value))
                {
                    _vehiculeConstruction.EstHomologueRoute = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le permis requis doit être mentionné.")]
        [DeniedValues([null], ErrorMessage = "Le permis requis doit être mentionné.")]
        public string PermisSpecial
        {
            get => _vehiculeConstruction.PermisSpecial.ToString();
            set
            {
                if (value != _vehiculeConstruction.PermisSpecial.ToString() && Validate(value))
                {
                    _vehiculeConstruction.PermisSpecial = (PermisType)Enum.Parse(typeof(PermisType), value);
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le type de véhicule doit être mentionné.")]
        [DeniedValues(["Autre"], ErrorMessage = "Le permis requis doit être mentionné.")]
        public string TypeVehicule
        {
            get => _vehiculeConstruction.TypeVehicule.ToString();
            set
            {
                if (value != _vehiculeConstruction.TypeVehicule.ToString() && Validate(value))
                {
                    _vehiculeConstruction.TypeVehicule = (ConstructionVehiculeType)Enum.Parse(typeof(ConstructionVehiculeType), value);
                    RaisePropertyChanged();
                }
            }
        }
    }
}

