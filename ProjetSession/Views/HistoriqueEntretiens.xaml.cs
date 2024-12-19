using Humanizer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using ProjetSession.Data;
using ProjetSession.Models;
using ProjetSession.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using static ProjetSession.Models.Utilities;

namespace ProjetSession.Views
{
    /// <summary>
    /// Page d'historique des entretiens pour chaque véhicule.
    /// Affiche les entretiens de chaque type pour un véhicule sélectionné.
    /// <b>Commentaire: <i>Je sais que cette page est plus ou moins mal structurée, mais je n'ai pas eu le temps de la refactoriser et de bien la refaire. Mais je suis au courant des problèmes qu'elle a.</i></b>
    /// </summary>
    public sealed partial class HistoriqueEntretiens : Page
    {
        public MainViewModelHistoriqueEntretiens ViewModel { get; }

        /// <summary>
        /// Constructeur de la page HistoriqueEntretiens.
        /// </summary>
        public HistoriqueEntretiens()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModelHistoriqueEntretiens(new DBEntretiensDataProvider(), new DBVehiculeTerrestresDataProvider());
            ChargerDonnees();
            ViewModel.RefreshListesEntretiens();
        }

        /// <summary>
        /// Charge les données de la page.
        /// </summary>
        public async void ChargerDonnees()
        {
            await ViewModel.ChargerDonneesAsync();
            ViewModel.VehiculeSelectionnee = ViewModel.ListeVehicules.FirstOrDefault();
            ViewModel.RefreshListesEntretiens();
        }

        /// <summary>
        /// Méthode appelée lors de la navigation vers la page HistoriqueEntretiens.
        /// Permet de mettre à jour les icônes (images) des types d'entretien selon le theme actuel.
        /// Si un véhicule est passé en paramètre, le véhicule devient le véhicule sélectionné dans la recherche et ces entretiens sont affichés.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e = null)
        {
            MettreAJourIconeSelonTheme(this, ActualTheme);
            base.OnNavigatedTo(e);
            if (e.Parameter is VehiculeTerrestreViewModel vehiculeTerrestreViewModel)
            {
                ViewModel.SetVehiculeSelectionnee(vehiculeTerrestreViewModel);
                ViewModel.RefreshListesEntretiens();
            }
        }

        /// <summary>
        /// Méthode qui met à jour la recherche de véhicule quand du texte est entré dans le AutoSuggestBox.
        /// Affiche les véhicules qui contiennent les lettres entrées comme suggestions.
        /// Permet de sélectionner un véhicule pour afficher ses entretiens.
        /// Appelé dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void VehicleSelecteur_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (ViewModel.ListeVinVehicules != null)
                {
                    List<string> vehiculesValide = new List<string>();
                    string[] lettresSeparees = sender.Text.ToLower().Split(" ");
                    foreach (string vehicule in ViewModel.ListeVinVehicules)
                    {
                        bool valide = lettresSeparees.All((lettre) =>
                        {
                            return vehicule.ToLower().Contains(lettre);
                        });
                        if (valide)
                        {
                            vehiculesValide.Add(vehicule);
                        }
                    }
                    if (vehiculesValide.Count == 0)
                    {
                        vehiculesValide.Add("Aucun résultat trouvé");
                    }
                    sender.ItemsSource = vehiculesValide;
                }
            }
        }

        /// <summary>
        /// Méthode qui met à jour le véhicule sélectionné quand une suggestion est choisie dans le AutoSuggestBox.
        /// Affiche les entretiens du véhicule sélectionné.
        /// Appelé dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void VehicleSelecteur_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            try
            {
                VehiculeTerrestreViewModel selection = ViewModel.ListeVehicules
                    .FirstOrDefault(v => v.Vin == args.SelectedItem.ToString());

                if (selection != null)
                {
                    ViewModel.VehiculeSelectionnee = selection;
                    ViewModel.RefreshListesEntretiens();
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Méthode qui est appelé quand le bouton annuler est cliqué dans le flyout d'ajout d'entretien.
        /// Cache le flyout.
        /// Appelé dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            flyoutAjouterEntretien.Hide();
        }

        /// <summary>
        /// Méthode qui est appelé quand le bouton ajouter est cliqué dans le flyout d'ajout d'entretien.
        /// Ajoute un nouvel entretien au véhicule sélectionné avec le type d'entretien sélectionné et les informations actuelles du véhicule.
        /// Appelé dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            int typeEntretien = comboBoxTypeEntretien.SelectedIndex + 1;
            DateTime dateEntretien = datePickerEntretien.Date.DateTime;
            int etatEntretien = ViewModel.VehiculeSelectionnee.EtatVehicule;
            int kilometrage = ((int)numberboxKilometrage.Value);
            string vin = ViewModel.VehiculeSelectionnee.Vin;

            ViewModel.AjouterEntretien(new EntretienViewModel(new Entretiens(typeEntretien, dateEntretien, etatEntretien, kilometrage, vin)));
            ViewModel.VehiculeSelectionnee.NouvelEntretienEtatVehicule(etatEntretien);
            flyoutAjouterEntretien.Hide();

            datePickerEntretien.Date = DateTimeOffset.Now;

        }

        /// <summary>
        /// Méthode qui affiche un dialog de confirmation pour la suppression d'un entretien.
        /// Si le bouton de confirmation est cliqué, l'entretien est supprimé du véhicule sélectionné et de la liste d'entretiens.
        /// Et aussi donné au ViewModel pour être supprimé de la base de données (Persistance).
        /// Elle est asynchrone pour empêcher le blocage de l'interface.
        /// </summary>
        /// <param name="typeEntretien">Le type d'entretien à supprimer (en string)</param>
        /// <returns>Le résultat du dialog de confirmation.</returns>
        private async Task<ContentDialogResult> ConfirmationSuppressionAsync(string typeEntretien)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Supprimer un entretien",
                Content = $"Êtes-vous sur de vouloir supprimer cet entretien {typeEntretien} de ce véhicule ?",
                PrimaryButtonText = "Annuler",
                SecondaryButtonText = "Supprimer cet entretien"
            };
            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
            {
                dialog.XamlRoot = XamlRoot;
            }
            return await dialog.ShowAsync();
        }



        /// <summary>
        /// Méthode qui est appelé quand le bouton supprimer est cliqué pour un entretien de type moteur.
        /// Appelle la méthode de confirmation de suppression et supprime l'entretien moteur si le bouton de confirmation est cliqué.
        /// Appelé dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SupprimerEntretienMoteur_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.EntretienMoteurSelectionnee != null)
            {
                if (await ConfirmationSuppressionAsync("moteur") == ContentDialogResult.Secondary)
                {
                    ViewModel.VehiculeSelectionnee.RetirerUnEntretienEtatVehicule(ViewModel.EntretienMoteurSelectionnee.TypeEntretien);
                    ViewModel.SupprimerEntretien(ViewModel.EntretienMoteurSelectionnee);
                    listViewEntretienMoteur.SelectedItem = null;
                    ViewModel.RefreshListesEntretiens(1);
                }
            }
        }

        /// <summary>
        /// Méthode qui est appelé quand le bouton supprimer est cliqué pour un entretien de type huile.
        /// Appelle la méthode de confirmation de suppression et supprime l'entretien huile si le bouton de confirmation est cliqué.
        /// Appelé dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SupprimerEntretienHuile_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.EntretienHuileSelectionnee != null)
            {
                if (await ConfirmationSuppressionAsync("huile") == ContentDialogResult.Secondary)
                {
                    ViewModel.VehiculeSelectionnee.RetirerUnEntretienEtatVehicule(ViewModel.EntretienHuileSelectionnee.TypeEntretien);
                    ViewModel.SupprimerEntretien(ViewModel.EntretienHuileSelectionnee);
                    listViewEntretienHuile.SelectedItem = null;
                    ViewModel.RefreshListesEntretiens(2);
                }
            }
        }

        /// <summary>
        /// Méthode qui est appelé quand le bouton supprimer est cliqué pour un entretien de type transmission.
        /// Appelle la méthode de confirmation de suppression et supprime l'entretien transmission si le bouton de confirmation est cliqué.
        /// Appelé dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SupprimerEntretienTransmission_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.EntretienTransmissionSelectionnee != null)
            {
                if (await ConfirmationSuppressionAsync("transmission") == ContentDialogResult.Secondary)
                {
                    ViewModel.VehiculeSelectionnee.RetirerUnEntretienEtatVehicule(ViewModel.EntretienTransmissionSelectionnee.TypeEntretien);
                    ViewModel.SupprimerEntretien(ViewModel.EntretienTransmissionSelectionnee);
                    listViewEntretienTransmission.SelectedItem = null;
                    ViewModel.RefreshListesEntretiens(3);
                }
            }
        }

        /// <summary>
        /// Méthode qui est appelé quand le bouton supprimer est cliqué pour un entretien de type pneus.
        /// Appelle la méthode de confirmation de suppression et supprime l'entretien pneus si le bouton de confirmation est cliqué.
        /// Appelé dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SupprimerEntretienPneus_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.EntretienPneusSelectionnee != null)
            {
                if (await ConfirmationSuppressionAsync("pneus") == ContentDialogResult.Secondary)
                {
                    ViewModel.VehiculeSelectionnee.RetirerUnEntretienEtatVehicule(ViewModel.EntretienPneusSelectionnee.TypeEntretien);
                    ViewModel.SupprimerEntretien(ViewModel.EntretienPneusSelectionnee);
                    listViewEntretienPneus.SelectedItem = null;
                    ViewModel.RefreshListesEntretiens(4);
                }
            }
        }
    }
}

 