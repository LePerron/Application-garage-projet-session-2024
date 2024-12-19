using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using ProjetSession.Models;
using System.Reflection;

namespace ProjetSession.ViewModels
{
    /// <summary>
    /// MainViewModel pour la page des détails d'un véhicule sélectionné dans l'inventaire.
    /// </summary>
    public class MainViewModelDetailsVehicule : ViewModelBase
    {
        public string Carrosserie;
        public string EstSportive;
        public string EstDecapotable;
        public string AEssuieGlacesArriere;
        public string ARoueDeSecours;
        public string EstHomologueRoute;
        public string PermisSpecial;
        public string TypeVehicule;
        public bool TypeVehiculeVisible = false;
        public string Domaine;
        public bool DomaineVisible = false;
        public string PoidsMax;
        public string ChargementMax;
        public string Hauteur;
        public bool HauteurMetre = false;
        public string VitesseMaxAutorisee;
        public bool VitesseMaxAutoriseeKmh = false;
        public string PermisRequis;


        /// Propriété pour obtenir l'état global du véhicule. 
        /// Retourne la valeur de l'état global du véhicule multipliée par 4 pour obtenir un pourcentage.
        /// Le x4 est pour obtenir un pourcentage sur 100 dans la view, la largeur est juste plus grande que l'état 100, donc le max de largeur est 400, -> (4 x etat) <- si etat = 100)
        public int EtatVehicule
        {
            get => _vehiculeSelectionnee.VehiculeTerrestre.ObtenirEtatGlobalVehicule() * 4;    
        }

        private VehiculeTerrestreViewModel _vehiculeSelectionnee;
        public VehiculeTerrestreViewModel VehiculeSelectionnee
        {
            get => _vehiculeSelectionnee;
            private set
            {
                if (_vehiculeSelectionnee != value)
                {
                    _vehiculeSelectionnee = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Le constructeur initialise les propriétés de la classe.
        /// Les propriétés sont initialisées à "N/A" pour éviter les erreurs de null.
        /// Les données sont statiques et ne changent pas.
        /// </summary>
        /// <param name="vehiculeSelectionnee"></param>
        public void SetVehiculeSelectionnee(VehiculeTerrestreViewModel vehiculeSelectionnee)
        {
            VehiculeSelectionnee = vehiculeSelectionnee;
            Carrosserie = ObtenirValeurPropriete("Carrosserie");
            EstSportive = ObtenirValeurPropriete("EstSportive");
            EstDecapotable = ObtenirValeurPropriete("EstDecapotable");    
            AEssuieGlacesArriere = ObtenirValeurPropriete("AEssuieGlacesArriere");    
            ARoueDeSecours = ObtenirValeurPropriete("ARoueDeSecours");    
            EstHomologueRoute = ObtenirValeurPropriete("EstHomologueRoute");
            PermisSpecial = ObtenirValeurPropriete("PermisSpecial") == "N/A" ? ObtenirValeurPropriete("PermisRequis") : ObtenirValeurPropriete("PermisSpecial");    
            TypeVehicule = ObtenirValeurPropriete("TypeVehicule");
            TypeVehiculeVisible = TypeVehicule != "N/A";
            Domaine = ObtenirValeurPropriete("Domaine");
            DomaineVisible = !TypeVehiculeVisible;
            PoidsMax = ObtenirValeurPropriete("PoidsMax");    
            ChargementMax = ObtenirValeurPropriete("ChargementMax");
            Hauteur = ObtenirValeurPropriete("Hauteur");
            HauteurMetre = Hauteur != "N/A";
            VitesseMaxAutorisee = ObtenirValeurPropriete("VitesseMaxAutorisee");
            VitesseMaxAutoriseeKmh = VitesseMaxAutorisee != "N/A";
        }

        /// <summary>
        /// Méthode pour obtenir la valeur d'une propriété d'un véhicule terrestre. 
        /// Si la propriété n'existe pas, retourne "N/A".
        /// </summary>
        /// <param name="nomPropriete">Nom de la propriété du véhicule à obtenir</param>
        /// <returns>Retourne la valeur de la propriété ou "N/A" si la propriété n'existe pas.</returns>
        public string ObtenirValeurPropriete(string nomPropriete)
        {
            string valeurPropriete = VehiculeSelectionnee.VehiculeTerrestre switch
            {
                VehiculePromenade vehiculePromenade => vehiculePromenade.GetType().GetProperties().Any(p => p.Name == nomPropriete) ? vehiculePromenade.GetType().GetProperty(nomPropriete).GetValue(vehiculePromenade).ToString() : "N/A",
                VehiculeConstruction vehiculeConstruction => vehiculeConstruction.GetType().GetProperties().Any(p => p.Name == nomPropriete) ? vehiculeConstruction.GetType().GetProperty(nomPropriete).GetValue(vehiculeConstruction).ToString() : "N/A",
                VehiculeLourd vehiculeLourd => vehiculeLourd.GetType().GetProperties().Any(p => p.Name == nomPropriete) ? vehiculeLourd.GetType().GetProperty(nomPropriete).GetValue(vehiculeLourd).ToString() : "N/A",
                _ => "N/A"
            };

            if (valeurPropriete == "False")
            {
                return "Non";
            }
            else if (valeurPropriete == "True")
            {
                return "Oui";
            }
            return valeurPropriete;
        }
    }
}
