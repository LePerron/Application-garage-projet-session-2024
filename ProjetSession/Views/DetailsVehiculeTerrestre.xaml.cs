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
    /// Page des détails d'un véhicule terrestre sélectionné à partir de la page GarageInventaire
    /// Permet d'ouvrir l'historique des entretiens du véhicule, de modifier, supprimer le véhicule dans la page Ajouter (avec le mode modification).
    /// Les données sont statiques et ne peuvent pas être modifiées.
    /// </summary>
    public sealed partial class DetailsVehiculeTerrestre : Page
    {
        public MainViewModelDetailsVehicule ViewModel { get; }

        // Permet de conserver le MainViewModelGarageInventaire pour pouvoir supprimer le véhicule du garage si jamais.
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
        /// Permet de récupérer le MainViewModelGarageInventaire ET de pour pouvoir récupérer le véhicule sélectionné.
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
        /// Permet de retourner une image par défaut si l'image de la voiture n'est pas trouvée.
        /// Appelée dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageVoiture_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            (sender as Image).Source = new BitmapImage(new Uri("ms-appx:///Assets/placeholderVoiture.jpg"));

        }

        /// <summary>
        /// Permet de naviguer vers la page AjouterAuGarage pour modifier le véhicule sélectionné avec le mode modification.
        /// Appelée dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjouterAuGarage), ViewModel.VehiculeSelectionnee);
        }

        /// <summary>
        /// Permet de supprimer le véhicule sélectionné du garage.
        /// Affiche un ContentDialog pour confirmer la suppression.
        /// Appelée dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialogueConfirmationSuppression = new ContentDialog
            {
                Title = "Supprimer le véhicule",
                Content = $"Êtes-vous sûr de vouloir supprimer {ViewModel.VehiculeSelectionnee.Marque.ToUpper()} {ViewModel.VehiculeSelectionnee.Modele.Pascalize()} {ViewModel.VehiculeSelectionnee.Couleur} ?",
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
        /// Permet de naviguer vers la page HistoriqueEntretiens pour afficher l'historique des entretiens du véhicule sélectionné.
        /// Appelée dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EffecturEntretien_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HistoriqueEntretiens), ViewModel.VehiculeSelectionnee);
        }
    }
}
