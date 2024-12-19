using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using ProjetSession.ViewModels;
using ProjetSession.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI;
using Windows.Graphics;
using Windows.Graphics.Display;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Collections;
using ProjetSession.Models;
using static ProjetSession.Models.Utilities;

namespace ProjetSession
{
    /// <summary>
    /// La fen�tre principale de l'application. 
    /// Elle contient le menu de navigation et le frame qui affiche les pages.
    /// Elle contient aussi le code pour changer le th�me de l'application. (PS: Je ne savais pas dans quoi je m'embarquais en voulant faire �a, mais c'est une fonctionnalit� cool !)
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        /// <summary>
        /// Constructeur de la fen�tre principale.
        /// Je mets le th�me de l'application � Light par d�faut pour emp�cher le flash de la fen�tre en mode Dark. (Je ne sais pas pourquoi �a flash, mais �a le fait, parfois.. Le manque de temps fait que je n'ai pas explor� plus)
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            (Content as FrameworkElement).RequestedTheme = ElementTheme.Light;
        }

        /// <summary>
        /// M�thode qui est appel�e lorsque la navigation �choue.
        /// Provient des notes de cours.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Impossible de charger la page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// M�thode qui est appel�e lorsque la fen�tre est charg�e.
        /// Provient des notes de cours.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += On_Navigated;
            NavView.SelectedItem = NavView.MenuItems[0];
        }

        /// <summary>
        /// M�thode qui est appel�e lorsque la s�lection de la navigation change.
        /// Provient des notes de cours.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                Type navPageType = Type.GetType(args.SelectedItemContainer.Tag.ToString());
                NavView_Navigate(navPageType, args.RecommendedNavigationTransitionInfo);
            }
        }

        /// <summary>
        /// M�thode qui est appel�e pour naviguer vers une page.
        /// Provient des notes de cours.
        /// </summary>
        /// <param name="navPageType"></param>
        /// <param name="transitionInfo"></param>
        private void NavView_Navigate(Type navPageType, NavigationTransitionInfo transitionInfo)
        {
            if (navPageType is not null)
            {
                ContentFrame.Navigate(navPageType, null, transitionInfo);
            }
        }

        /// <summary>
        /// M�thode qui est appel�e lorsque la navigation est termin�e. 
        /// Provient des notes de cours.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType != null)
            {
                try
                {
                    if (NavView.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.Equals(ContentFrame.SourcePageType.FullName.ToString())) != null)
                    {
                        NavView.SelectedItem = NavView.MenuItems
                                            .OfType<NavigationViewItem>()
                                            .First(i => i.Tag.Equals(ContentFrame.SourcePageType.FullName.ToString()));

                        NavView.Header = ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
                    }
                }
                catch (Exception)
                {
                    NavView.SelectedItem = null;
                    NavView.Header = "Page inconnue";
                }
            }
        }

        /// <summary>
        /// M�thode qui est appel�e pour changer le th�me de l'application.
        /// Elle change le th�me de l'application et met � jour les ic�nes (images) des type d'entretiens.
        /// </summary>
        /// <remarks>
        ///     <i><b>Finalement pour changer le th�me de l'application pendant qu'elle est en marche, j'ai trouv� pourquoi FrameWorkElement et pas Window. Parce que je ne peux pas changer le th�me de la fen�tre principale, mais je peux changer le th�me de son contenu alors je contourne la limitation de WinUI en changeant le contenu.</b></i>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Changer_theme(object sender, RoutedEventArgs e)
        {
            if (Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = frameworkElement.ActualTheme == ElementTheme.Dark ? ElementTheme.Light : ElementTheme.Dark;
                MettreAJourIconeSelonTheme(frameworkElement, frameworkElement.ActualTheme);
            }
        }
    }
}
