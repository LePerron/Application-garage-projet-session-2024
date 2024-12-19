using Humanizer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using ProjetSession.Models;
using ProjetSession.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;

namespace ProjetSession.Views
{
    /// <summary>
    /// Page des d�tails d'un v�hicule terrestre s�lectionn� � partir de la page GarageInventaire
    /// Permet d'ouvrir l'historique des entretiens du v�hicule, de modifier, supprimer le v�hicule dans la page Ajouter (avec le mode modification).
    /// Les donn�es sont statiques et ne peuvent pas �tre modifi�es.
    /// </summary>
    public sealed partial class DetailsVehiculeTerrestre : Page
    {
        public MainViewModelDetailsVehicule ViewModel { get; }

        // Permet de conserver le MainViewModelGarageInventaire pour pouvoir supprimer le v�hicule du garage si jamais.
        private MainViewModelGarageInventaire _mainViewModelGarageInventaire;

        /// <summary>
        /// Constructeur de la page DetailsVehiculeTerrestre
        /// </summary>
        public DetailsVehiculeTerrestre()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModelDetailsVehicule();
            DataContext = ViewModel;
        }

        /// <summary>
        /// Permet de r�cup�rer le MainViewModelGarageInventaire ET de pour pouvoir r�cup�rer le v�hicule s�lectionn�.
        /// </summary>
        /// <param name="e">Le MainViewModelGarageInventaire</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is MainViewModelGarageInventaire mainViewModelGarageInventaire)
            {
                _mainViewModelGarageInventaire = mainViewModelGarageInventaire;
                ViewModel.SetVehiculeSelectionnee(_mainViewModelGarageInventaire.VehiculeSelectionnee);
            }
        }

        /// <summary>
        /// Permet de retourner une image par d�faut si l'image de la voiture n'est pas trouv�e.
        /// Appel�e dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageVoiture_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            (sender as Image).Source = new BitmapImage(new Uri("ms-appx:///Assets/placeholderVoiture.jpg"));

        }

        /// <summary>
        /// Permet de naviguer vers la page AjouterAuGarage pour modifier le v�hicule s�lectionn� avec le mode modification.
        /// Appel�e dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjouterAuGarage), ViewModel.VehiculeSelectionnee);
        }

        /// <summary>
        /// Permet de supprimer le v�hicule s�lectionn� du garage.
        /// Affiche un ContentDialog pour confirmer la suppression.
        /// Appel�e dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialogueConfirmationSuppression = new ContentDialog
            {
                Title = "Supprimer le v�hicule",
                Content = $"�tes-vous s�r de vouloir supprimer {ViewModel.VehiculeSelectionnee.Marque.ToUpper()} {ViewModel.VehiculeSelectionnee.Modele.Pascalize()} {ViewModel.VehiculeSelectionnee.Couleur} ?",
                PrimaryButtonText = "Supprimer",
                CloseButtonText = "Annuler"
            };

            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8)) // Provient des notes de cours
            {
                dialogueConfirmationSuppression.XamlRoot = this.XamlRoot;
            }

            ContentDialogResult resultat = await dialogueConfirmationSuppression.ShowAsync();
            if (resultat == ContentDialogResult.Primary)
            {
                Frame.Navigate(typeof(GarageInventaire));
                _mainViewModelGarageInventaire.SupprimerUnVehiculeDuGarage(ViewModel.VehiculeSelectionnee);
            }
        }

        /// <summary>
        /// Permet de naviguer vers la page HistoriqueEntretiens pour afficher l'historique des entretiens du v�hicule s�lectionn�.
        /// Appel�e dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EffecturEntretien_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HistoriqueEntretiens), ViewModel.VehiculeSelectionnee);
        }
    }
}
