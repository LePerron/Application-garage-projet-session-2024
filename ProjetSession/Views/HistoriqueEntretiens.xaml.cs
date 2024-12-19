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
    /// Page d'historique des entretiens pour chaque v�hicule.
    /// Affiche les entretiens de chaque type pour un v�hicule s�lectionn�.
    /// <b>Commentaire: <i>Je sais que cette page est plus ou moins mal structur�e, mais je n'ai pas eu le temps de la refactoriser et de bien la refaire. Mais je suis au courant des probl�mes qu'elle a.</i></b>
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
        /// Charge les donn�es de la page.
        /// </summary>
        public async void ChargerDonnees()
        {
            await ViewModel.ChargerDonneesAsync();
            ViewModel.VehiculeSelectionnee = ViewModel.ListeVehicules.FirstOrDefault();
            ViewModel.RefreshListesEntretiens();
        }

        /// <summary>
        /// M�thode appel�e lors de la navigation vers la page HistoriqueEntretiens.
        /// Permet de mettre � jour les ic�nes (images) des types d'entretien selon le theme actuel.
        /// Si un v�hicule est pass� en param�tre, le v�hicule devient le v�hicule s�lectionn� dans la recherche et ces entretiens sont affich�s.
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
        /// M�thode qui met � jour la recherche de v�hicule quand du texte est entr� dans le AutoSuggestBox.
        /// Affiche les v�hicules qui contiennent les lettres entr�es comme suggestions.
        /// Permet de s�lectionner un v�hicule pour afficher ses entretiens.
        /// Appel� dans le XAML.
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
                        vehiculesValide.Add("Aucun r�sultat trouv�");
                    }
                    sender.ItemsSource = vehiculesValide;
                }
            }
        }

        /// <summary>
        /// M�thode qui met � jour le v�hicule s�lectionn� quand une suggestion est choisie dans le AutoSuggestBox.
        /// Affiche les entretiens du v�hicule s�lectionn�.
        /// Appel� dans le XAML.
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
        /// M�thode qui est appel� quand le bouton annuler est cliqu� dans le flyout d'ajout d'entretien.
        /// Cache le flyout.
        /// Appel� dans le XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            flyoutAjouterEntretien.Hide();
        }

        /// <summary>
        /// M�thode qui est appel� quand le bouton ajouter est cliqu� dans le flyout d'ajout d'entretien.
        /// Ajoute un nouvel entretien au v�hicule s�lectionn� avec le type d'entretien s�lectionn� et les informations actuelles du v�hicule.
        /// Appel� dans le XAML.
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
        /// M�thode qui affiche un dialog de confirmation pour la suppression d'un entretien.
        /// Si le bouton de confirmation est cliqu�, l'entretien est supprim� du v�hicule s�lectionn� et de la liste d'entretiens.
        /// Et aussi donn� au ViewModel pour �tre supprim� de la base de donn�es (Persistance).
        /// Elle est asynchrone pour emp�cher le blocage de l'interface.
        /// </summary>
        /// <param name="typeEntretien">Le type d'entretien � supprimer (en string)</param>
        /// <returns>Le r�sultat du dialog de confirmation.</returns>
        private async Task<ContentDialogResult> ConfirmationSuppressionAsync(string typeEntretien)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Supprimer un entretien",
                Content = $"�tes-vous sur de vouloir supprimer cet entretien {typeEntretien} de ce v�hicule ?",
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
        /// M�thode qui est appel� quand le bouton supprimer est cliqu� pour un entretien de type moteur.
        /// Appelle la m�thode de confirmation de suppression et supprime l'entretien moteur si le bouton de confirmation est cliqu�.
        /// Appel� dans le XAML.
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
        /// M�thode qui est appel� quand le bouton supprimer est cliqu� pour un entretien de type huile.
        /// Appelle la m�thode de confirmation de suppression et supprime l'entretien huile si le bouton de confirmation est cliqu�.
        /// Appel� dans le XAML.
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
        /// M�thode qui est appel� quand le bouton supprimer est cliqu� pour un entretien de type transmission.
        /// Appelle la m�thode de confirmation de suppression et supprime l'entretien transmission si le bouton de confirmation est cliqu�.
        /// Appel� dans le XAML.
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
        /// M�thode qui est appel� quand le bouton supprimer est cliqu� pour un entretien de type pneus.
        /// Appelle la m�thode de confirmation de suppression et supprime l'entretien pneus si le bouton de confirmation est cliqu�.
        /// Appel� dans le XAML.
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

 