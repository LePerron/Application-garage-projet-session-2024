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
    /// Page principale du garage, affiche les v�hicules terrestres dans le garage
    /// Contient un AutoSuggestBox pour filtrer les v�hicules par mod�le
    /// Lorsque double-tap sur un v�hicule, navigue vers la page de d�tails du v�hicule
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
        /// M�thode appel�e lorsque la page est charg�e permettant de charger les donn�es
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Root_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ChargerDonnees();
        }

        /// <summary>
        /// M�thode qui retourne l'image par d�faut si l'image de la voiture n'est pas trouv�e
        /// Elle est appel�e depuis le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageVoiture_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            (sender as Image).Source = new BitmapImage(new Uri("ms-appx:///Assets/placeholderVoiture.jpg"));

        }

        /// <summary>
        /// M�thode appel�e lorsqu'un v�hicule est double-tapp� pour naviguer vers la page de d�tails du v�hicule
        /// Envoie le ViewModel en param�tre pour pouvoir afficher les d�tails du v�hicule
        /// Appel�e depuis le XAML
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
        /// M�thode appel�e lors du clic sur le bouton de suppression d'un v�hicule
        /// Appelle la m�thode de confirmation de suppression qui affiche une bo�te de dialogue
        /// Appel�e depuis le XAML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerVehicule_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationSupprimerVehicule();
        }

        /// <summary>
        /// M�thode qui affiche une bo�te de dialogue de confirmation de suppression d'un v�hicule
        /// Supprime le v�hicule si supprim� est cliqu�.
        /// </summary>
        private async void ConfirmationSupprimerVehicule()
        {
            ContentDialog dialogueConfirmationSuppression = new ContentDialog
            {
                Title = "Supprimer le v�hicule",
                Content = $"�tes-vous s�r de vouloir supprimer {ViewModel.VehiculeSelectionnee.Marque.ToString().ToUpper()} {ViewModel.VehiculeSelectionnee.Modele.Pascalize()} {ViewModel.VehiculeSelectionnee.Couleur} ?",
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
        /// M�thode appel�e lors du clic sur le bouton d'ajout d'un v�hicule
        /// Envoie vers la page d'ajout d'un v�hicule.
        /// Appel�e depuis le XAML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjouterAuGarage));
        }


        /// <summary>
        /// M�thode qui filtre les v�hicules selon le texte entr� dans l'AutoSuggestBox et met � jour la source de l'AutoSuggestBox
        /// Valide si le texte est vide, sinon, filtre les v�hicules selon le mod�le.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mettreAJourItemSource">Si la m�thode doit mettre la source du AutoSuggestBox ou non</param>
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
                modelesValide.Add("Aucun r�sultat trouv�");
            }
            if (mettreAJourItemSource)
            {
                sender.ItemsSource = modelesValide;
            }
            GridViewInventaire.ItemsSource = ViewModel.VehiculesValides;
        }

        /// <summary>
        /// M�thode appel�e lorsque du text est entr� dans l'AutoSuggestBox.
        /// Appelle la m�thode de filtre des v�hicules.
        /// Appel�e depuis le XAML.
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
        /// M�thode appel�e lorsqu'un �l�ment est s�lectionn� dans l'AutoSuggestBox.
        /// Appelle la m�thode de filtre des v�hicules pour valider les r�sultats affich�s dans le GridView.
        /// Appel�e depuis le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void VehicleSelecteur_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem.ToString() == "Aucun r�sultat trouv�")
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
