using ProjetSession.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using ProjetSession.Models.Convertisseurs;
using Microsoft.UI.Xaml;

namespace ProjetSession.ViewModels
{
    /// <summary>
    /// ViewModel pour les entretiens.
    /// Hérite de ViewModelBase.
    /// </summary>
    public class EntretienViewModel : ViewModelBase
    {

        /// <summary>
        /// Constructeur de la classe EntretienViewModel. 
        /// </summary>
        /// <param name="entretien"></param>
        public EntretienViewModel(Entretiens entretien)
        {
            _entretien = entretien;
        }

        private Entretiens _entretien;
        public Entretiens Entretien
        {
            get => _entretien;
            set
            {
                if (_entretien != value)
                {
                    _entretien = value;
                    RaisePropertyChanged();
                }
            }
        }


        public int Id 
        {
            get => _entretien.Id;
        }

        public int TypeEntretien
        {
            get => _entretien.TypeEntretien;
        }

        public string Date
        {
            get => Utilities.DateToString(_entretien.Date);

        }
        public int Etat
        {
            get =>_entretien.Etat;
        }

        public int Kilometrage
        {
            get => _entretien.Kilometrage;
        }

        public string Vin
        {
            get => _entretien.Vin;
        }

        /// <summary>
        /// Propriété pour obtenir l'image de l'entretien selon le type d'entretien.
        /// Si le type d'entretien est inconnu, retourne une image par défaut.
        /// </summary>
        public string ImageTypeEntretien
        {
            get
            {
                return TypeEntretien switch
                {
                    1 => "ms-appx:///Assets/Icon-moteur_Clair.png",
                    2 => "ms-appx:///Assets/Icon-huile-moteur_Clair.png",
                    3 => "ms-appx:///Assets/Icon-transmission_Clair.png",
                    4 => "ms-appx:///Assets/Icon-pneus_Clair.png",
                    _ => "ms-appx:///Assets/StoreLogo.png",
                };
            }
        }
    }
}
