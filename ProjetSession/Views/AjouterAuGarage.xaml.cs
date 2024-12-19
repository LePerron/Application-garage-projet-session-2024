using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ProjetSession.Data;
using ProjetSession.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI;
using Windows.Devices.Enumeration;
using Humanizer;
using ProjetSession.Models.Types;
using System.ComponentModel.DataAnnotations;
using System.Security;
using Windows.UI;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using ProjetSession.Models;

namespace ProjetSession.Views
{
    /// <summary>
    /// La page permettant d'ajouter un véhicule au garage de l'utilisateur. 
    /// Elle est divisée en 3 pages pour faciliter visuellement l'ajout de véhicule.
    /// En fonction du type de véhicule sélectionné, des champs de plus peuvent être affichés.
    /// Si la page doit modifier un véhicule, les champs seront remplis avec les informations du véhicule à modifier dans le bon type de véhicule.
    /// Contient sa propre logique pour la validation des champs lors du clic sur le bouton d'ajout et de changement de page.
    /// </summary>
    public sealed partial class AjouterAuGarage : Page
    {
        public MainViewModelAjouterAuGarage ViewModel { get; }

        public bool FormulaireEnModification = false;

        List<List<string>> ProprieteDesPages = 
            [
                ["Vin", "Modele", "Couleur", "Marque", "Annee", "Carburant", "Consommation", "PrixConstructeurAchat"],
                ["BoiteVitesse", "Transmission", "NombreCylindres", "ChevauxVapeur", "Kilometrage"]
            ];

        /// <summary>
        /// Constructeur de la page AjouterAuGarage
        /// </summary>
        public AjouterAuGarage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModelAjouterAuGarage(new DBVehiculeTerrestresDataProvider());
        }

        /// <summary>
        /// Méthode appelée lors de la navigation vers la page AjouterAuGarage. 
        /// Si un véhicule est passé en paramètre, la page est en mode modification.
        /// Elle permet de remplir les champs de la page si un véhicule est en cours de modification.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e=null)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is VehiculeTerrestreViewModel vehiculeTerrestreViewModel)
            {
                FormulaireEnModification = true;
                VehiculeTerrestre vehicule = ViewModel.ModifierTypeVehiculeSelectionnee(vehiculeTerrestreViewModel);

                if (vehicule is VehiculeConstruction vehiculeConstruction)
                {
                    comboboxTypeVehicule.SelectedIndex = ViewModel.TypeFonctionVehiculeConstruction.IndexOf(vehiculeConstruction.TypeVehicule.ToString());
                    comboboxPermisSpecial.SelectedIndex = ViewModel.TypePermis.IndexOf(vehiculeConstruction.PermisSpecial.ToString());
                }
                else if (vehicule is VehiculeLourd vehiculeLourd)
                {
                    comboboxPermisRequis.SelectedIndex = ViewModel.TypePermis.IndexOf(vehiculeLourd.PermisRequis.ToString());
                }
                comboboxMarque.SelectedIndex = ViewModel.Marques.IndexOf(vehiculeTerrestreViewModel.Marque);
                comboboxCouleur.SelectedIndex = ViewModel.Couleurs.IndexOf(vehiculeTerrestreViewModel.Couleur);
                comboboxTransmission.SelectedIndex = ViewModel.Transmissions.IndexOf(vehiculeTerrestreViewModel.Transmission);
                comboboxBoiteVitesse.SelectedIndex = ViewModel.BoiteVitesses.IndexOf(vehiculeTerrestreViewModel.BoiteVitesse);
                comboboxCarburant.SelectedIndex = ViewModel.Carburants.IndexOf(vehiculeTerrestreViewModel.Carburant);

                TitrePageAjoutModification.Text = "Modifier un véhicule";
                BtnAnnuler.Content = "Annuler les modifications";
                BtnModifier.Visibility = Visibility.Visible;
                BtnAjouter.Visibility = Visibility.Collapsed;
                ComboboxPageAjout.IsEnabled = false;
                TextBoxVin.IsEnabled = false;
            }
        }

        /// <summary>
        /// Méthode appelée lors du changement de page dans le formulaire d'ajout de véhicule.
        /// Permet de modifier les champs affichés en fonction de la page actuelle. (Dans le MainViewModel)
        /// </summary>
        public void ModifierLaPage()
        {
            switch (ViewModel.Page)
            {
                case 1:
                    TitrePageAjoutModification.Visibility = Visibility.Visible;
                    ComboboxPageAjout.Visibility = Visibility.Visible;
                    Page1.Visibility = Visibility.Visible;
                    Page2.Visibility = Visibility.Collapsed;
                    Page3.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    TitrePageAjoutModification.Visibility = Visibility.Collapsed;
                    ComboboxPageAjout.Visibility = Visibility.Collapsed;
                    Page1.Visibility = Visibility.Collapsed;
                    Page2.Visibility = Visibility.Visible;
                    Page3.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    TitrePageAjoutModification.Visibility = Visibility.Collapsed;
                    ComboboxPageAjout.Visibility = Visibility.Collapsed;
                    Page1.Visibility = Visibility.Collapsed;
                    Page2.Visibility = Visibility.Collapsed;
                    Page3.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// Méthode appelée lors du clic sur le bouton de page précédente.
        /// Appelée dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagePrecedente_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Page--;
            ModifierLaPage();
        }

        /// <summary>
        /// Méthode appelée lors du clic sur le bouton de page suivante.
        /// Appelée dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageSuivante_Click(object sender, RoutedEventArgs e)
        {
            if (FormulaireEnModification)
            {
                if (!ViewModel.NouveauVehicule.HasErrors)
                {
                    ViewModel.Page++;
                    ModifierLaPage();
                }
            }
            else if (!PageContientErreurs())
            {
                ViewModel.Page++;
                ModifierLaPage();
            }
        }

        /// <summary>
        /// Méthode appelée lors du changement de sélection du type de véhicule dans le combobox.
        /// Appelée dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeVehiculeSelectionne_SelectionChanged(object sender = null, SelectionChangedEventArgs e = null)
        {
            ComboboxPageAjout.IsEnabled = false;
            ViewModel.Page++;
            ModifierLaPage();
        }

        /// <summary>
        /// Méthode appelée lors du clic sur le bouton d'ajout de véhicule. 
        /// Elle valide les champs du formulaire et ajoute le véhicule au garage.
        /// Appelée dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterVehicule_Click(object sender, RoutedEventArgs e)
        {
            foreach (PropertyInfo propriete in ViewModel.NouveauVehicule.GetType().GetProperties())
            {
                ViewModel.NouveauVehicule.Validate(ViewModel.NouveauVehicule.GetType().GetProperty(propriete.Name).GetValue(ViewModel.NouveauVehicule), propriete.Name);
            }

            if (ViewModel.NouveauVehicule.EstValide)
            {
                ViewModel.AjouterAuGarage();
                Frame.Navigate(typeof(GarageInventaire));
            }

        }

        /// <summary>
        /// Méthode appelée lors du clic sur le bouton Modifier quand la page est en mode modification.
        /// Elle valide les champs du formulaire et modifie le véhicule dans le garage.
        /// Appelée dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifierVehicule_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.NouveauVehicule.EstValide)
            {
                ViewModel.ModifierVehicule();
                Frame.Navigate(typeof(GarageInventaire));
            }
        }

        /// <summary>
        /// Méthode appelée lors du clic sur le bouton Annuler.
        /// Elle annule l'ajout ou la modification du véhicule et retourne à la page GarageInventaire.
        /// Appelée dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GarageInventaire));
        }

        /// <summary>
        /// Méthode qui vérifie si la page contient des erreurs de validation.
        /// Selon les propriétés de la page actuelle du formulaire, elle valide les champs selon la liste.
        /// </summary>
        /// <returns>True si la page contient des erreurs, false sinon.</returns>
        public bool PageContientErreurs()
        {
            bool estValide = true;
            foreach (string propriete in ProprieteDesPages[ViewModel.Page - 1])
            {
                estValide &= ViewModel.NouveauVehicule.Validate(ViewModel.NouveauVehicule.GetType().GetProperty(propriete).GetValue(ViewModel.NouveauVehicule), propriete);
            }
            return !estValide;
        }
    }
}
