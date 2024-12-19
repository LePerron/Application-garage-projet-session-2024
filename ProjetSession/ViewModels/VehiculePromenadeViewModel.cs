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
    /// ViewModel pour les véhicules de promenade.
    /// Avec les propriétés de validation et les erreurs de validation avec leurs messages.
    /// Hérite de VehiculeTerrestreViewModel.
    /// </summary>
    public class VehiculePromenadeViewModel : VehiculeTerrestreViewModel
    {
        private VehiculePromenade _vehiculePromenade;

        /// <summary>
        /// Constructeur du ViewModel des véhicules de promenade.
        /// </summary>
        /// <param name="vehiculePromenade"></param>
        public VehiculePromenadeViewModel(VehiculePromenade vehiculePromenade) : base(vehiculePromenade)
        {
            _vehiculePromenade = vehiculePromenade;

            _errors.Add("Carrosserie", new ObservableCollection<ValidationResult>());
            _errors.Add("EstSportive", new ObservableCollection<ValidationResult>());
            _errors.Add("EstDecapotable", new ObservableCollection<ValidationResult>());
            _errors.Add("AEssuieGlacesArriere", new ObservableCollection<ValidationResult>());
            _errors.Add("ARoueDeSecours", new ObservableCollection<ValidationResult>());
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'option carrosserie ne peut être vide.")]
        [DeniedValues(["Autre"], ErrorMessage = "L'option carrosserie ne peut être vide.")]
        public string Carrosserie
        {
            get => _vehiculePromenade.Carrosserie.ToString();
            set
            {
                if (value != _vehiculePromenade.Carrosserie.ToString() && Validate(value))
                {
                    _vehiculePromenade.Carrosserie = (CarrosserieVehiculePromenade)Enum.Parse(typeof(CarrosserieVehiculePromenade), value);
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'option sportive ne peut être vide.")]
        [DeniedValues([null], ErrorMessage = "L'option sportive ne peut être vide.")]
        public bool? EstSportive
        {
            get => _vehiculePromenade.EstSportive;
            set
            {
                if (value != _vehiculePromenade.EstSportive && Validate(value))
                {
                    _vehiculePromenade.EstSportive = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'option décapotable ne peut être vide.")]
        [DeniedValues([null], ErrorMessage = "L'option décapotable ne peut être vide.")]
        public bool? EstDecapotable
        {
            get => _vehiculePromenade.EstDecapotable;
            set
            {
                if (value != _vehiculePromenade.EstDecapotable && Validate(value))
                {
                    _vehiculePromenade.EstDecapotable = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'option essuie-glaces arrière ne peut être vide.")]
        [DeniedValues([null], ErrorMessage = "L'option essuie-glaces arrière ne peut être vide.")]
        public bool? AEssuieGlacesArriere
        {
            get => _vehiculePromenade.AEssuieGlacesArriere;
            set
            {
                if (value != _vehiculePromenade.AEssuieGlacesArriere && Validate(value))
                {
                    _vehiculePromenade.AEssuieGlacesArriere = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'option roue de secours équipée ne peut être vide.")]
        [DeniedValues([null], ErrorMessage = "L'option roue de secours équipée ne peut être vide.")]
        public bool? ARoueDeSecours
        {
            get => _vehiculePromenade.ARoueDeSecours;
            set
            {
                if (value != _vehiculePromenade.ARoueDeSecours && Validate(value))
                {
                    _vehiculePromenade.ARoueDeSecours = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
