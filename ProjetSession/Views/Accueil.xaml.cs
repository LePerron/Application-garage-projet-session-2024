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
using ProjetSession.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static ProjetSession.Models.Utilities;

namespace ProjetSession.Views
{
    /// <summary>
    /// Page d'accueil de l'application qui affiche les informations sur les v�hicules et les entretiens � venir
    /// C'est un tableau de bord.
    /// Toutes les informations sont r�cup�r�es depuis la base de donn�es. 
    /// </summary>
    public sealed partial class Accueil : Page
    {
        public MainViewModelAccueil ViewModel { get; }

        /// <summary>
        /// Constructeur de la page d'accueil
        /// </summary>
        public Accueil()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModelAccueil(new DBVehiculeTerrestresDataProvider(), new DBEntretiensDataProvider());
            Loaded += Accueil_Loaded;
            MettreAJourIconeSelonTheme(this, ActualTheme);
        }

        /// <summary>
        /// M�thode qui est appel�e lorsque la page est charg�e et qui met � jour les ic�nes (images) selon le th�me actuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accueil_Loaded(object sender, RoutedEventArgs e)
        {
            MettreAJourIconeSelonTheme(this, ActualTheme);
        }

        /// <summary>
        /// M�thode qui est appel�e lorsque la page est navigu�e vers cette page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e = null)
        {
            base.OnNavigatedTo(e);
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
    }
}
