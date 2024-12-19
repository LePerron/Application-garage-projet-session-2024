using Humanizer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using ProjetSession.Data;
using ProjetSession.Models;
using ProjetSession.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;

namespace ProjetSession.Views
{
    /// <summary>
    /// Page principale du garage, affiche les véhicules terrestres dans le garage
    /// Contient un AutoSuggestBox pour filtrer les véhicules par modèle
    /// Lorsque double-tap sur un véhicule, navigue vers la page de détails du véhicule
    /// </summary>
    public sealed partial class GarageInventaire : Page
    {
        public MainViewModelGarageInventaire ViewModel { get; }

        /// <summary>
        /// Constructeur de la page GarageInventaire
        /// </summary>
        public GarageInventaire()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModelGarageInventaire(new DBVehiculeTerrestresDataProvider(), new DBEntretiensDataProvider());
            root.Loaded += Root_Loaded;
            ViewModel.MettreAJourEtatVehiculesSiNecessaire();
        }

        /// <summary>
        /// Méthode appelée lorsque la page est chargée permettant de charger les données
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Root_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ChargerDonnees();
        }

        /// <summary>
        /// Méthode qui retourne l'image par défaut si l'image de la voiture n'est pas trouvée
        /// Elle est appelée depuis le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageVoiture_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            (sender as Image).Source = new BitmapImage(new Uri("ms-appx:///Assets/placeholderVoiture.jpg"));

        }

        /// <summary>
        /// Méthode appelée lorsqu'un véhicule est double-tappé pour naviguer vers la page de détails du véhicule
        /// Envoie le ViewModel en paramètre pour pouvoir afficher les détails du véhicule
        /// Appelée depuis le XAML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewItem_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

            if (ViewModel.VehiculeSelectionnee != null)
            {
                Frame.Navigate(typeof(DetailsVehiculeTerrestre), ViewModel);
            }
        }

        /// <summary>
        /// Méthode appelée lors du clic sur le bouton de suppression d'un véhicule
        /// Appelle la méthode de confirmation de suppression qui affiche une boîte de dialogue
        /// Appelée depuis le XAML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerVehicule_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationSupprimerVehicule();
        }

        /// <summary>
        /// Méthode qui affiche une boîte de dialogue de confirmation de suppression d'un véhicule
        /// Supprime le véhicule si supprimé est cliqué.
        /// </summary>
        private async void ConfirmationSupprimerVehicule()
        {
            ContentDialog dialogueConfirmationSuppression = new ContentDialog
            {
                Title = "Supprimer le véhicule",
                Content = $"Êtes-vous sûr de vouloir supprimer {ViewModel.VehiculeSelectionnee.Marque.ToString().ToUpper()} {ViewModel.VehiculeSelectionnee.Modele.Pascalize()} {ViewModel.VehiculeSelectionnee.Couleur} ?",
                PrimaryButtonText = "Supprimer",
                CloseButtonText = "Annuler"
            };

            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
            {
                dialogueConfirmationSuppression.XamlRoot = this.XamlRoot;
            }

            ContentDialogResult result = await dialogueConfirmationSuppression.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ViewModel.SupprimerUnVehiculeDuGarage();
            }
        }

        /// <summary>
        /// Méthode appelée lors du clic sur le bouton d'ajout d'un véhicule
        /// Envoie vers la page d'ajout d'un véhicule.
        /// Appelée depuis le XAML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjouterAuGarage));
        }


        /// <summary>
        /// Méthode qui filtre les véhicules selon le texte entré dans l'AutoSuggestBox et met à jour la source de l'AutoSuggestBox
        /// Valide si le texte est vide, sinon, filtre les véhicules selon le modèle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mettreAJourItemSource">Si la méthode doit mettre la source du AutoSuggestBox ou non</param>
        private void FilterVehicles(AutoSuggestBox sender, bool mettreAJourItemSource = true)
        {
            if (ViewModel.ListeModeleVehicules == null) return;

            List<string> modelesValide = new List<string>();
            ViewModel.VehiculesValides.Clear();

            if (string.IsNullOrWhiteSpace(sender.Text))
            {
                modelesValide.AddRange(ViewModel.ListeModeleVehicules);
                foreach (VehiculeTerrestreViewModel vehicle in ViewModel.VehiculeTerrestres)
                {
                    ViewModel.VehiculesValides.Add(vehicle);
                }
            }
            else
            {
                string recherche = sender.Text.ToLower();
                foreach (string modele in ViewModel.ListeModeleVehicules)
                {
                    if (modele.Contains(recherche, StringComparison.CurrentCultureIgnoreCase))
                    {
                        modelesValide.Add(modele.CapitalizePremiereLettre());
                        foreach (VehiculeTerrestreViewModel vehicule in ViewModel.VehiculeTerrestres.Where(v => v.Modele == modele))
                        {
                            ViewModel.VehiculesValides.Add(vehicule);
                        }
                    }
                }
            }
            if (modelesValide.Count == 0)
            {
                modelesValide.Add("Aucun résultat trouvé");
            }
            if (mettreAJourItemSource)
            {
                sender.ItemsSource = modelesValide;
            }
            GridViewInventaire.ItemsSource = ViewModel.VehiculesValides;
        }

        /// <summary>
        /// Méthode appelée lorsque du text est entré dans l'AutoSuggestBox.
        /// Appelle la méthode de filtre des véhicules.
        /// Appelée depuis le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void VehicleSelecteur_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                FilterVehicles(sender);
            }
        }

        /// <summary>
        /// Méthode appelée lorsqu'un élément est sélectionné dans l'AutoSuggestBox.
        /// Appelle la méthode de filtre des véhicules pour valider les résultats affichés dans le GridView.
        /// Appelée depuis le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void VehicleSelecteur_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem.ToString() == "Aucun résultat trouvé")
            {
                return;
            }

            ViewModel.VehiculesValides.Clear();

            ViewModel.VehiculeTerrestres
                .Where(v => v.Modele.Equals(args.SelectedItem.ToString(), StringComparison.CurrentCultureIgnoreCase))
                .ToList()
                .ForEach(vehiculeValide => ViewModel.VehiculesValides.Add(vehiculeValide));

            GridViewInventaire.ItemsSource = ViewModel.VehiculesValides;
        }
    }
}
