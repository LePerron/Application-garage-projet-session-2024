using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Controls;
using System.Linq;


namespace ProjetSession.Models
{
    /// <summary>
    /// Classe utilitaire contenant des méthodes utiles pour le projet.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Convertit une chaîne de caractères en un nom de fichier valide en remplaçant les caractères spéciaux.
        /// </summary>
        /// <param name="chaineCaracteres">La chaîne de caractères à convertir.</param>
        /// <returns>Le nom de fichier converti.</returns>
        static public string ConvertirChaineCaracteresToNomFichier(string chaineCaracteres)
        {
            chaineCaracteres = chaineCaracteres.Replace("<", "_");
            chaineCaracteres = chaineCaracteres.Replace(" ", "_");
            chaineCaracteres = chaineCaracteres.Replace(">", "_");
            chaineCaracteres = chaineCaracteres.Replace(":", "_");
            chaineCaracteres = chaineCaracteres.Replace('"', '_');
            chaineCaracteres = chaineCaracteres.Replace(@"/", "_");
            chaineCaracteres = chaineCaracteres.Replace(@"\", "_");
            chaineCaracteres = chaineCaracteres.Replace("|", "_");
            chaineCaracteres = chaineCaracteres.Replace("?", "_");
            return chaineCaracteres.Replace("*", "_");
        }

        /// <summary>
        /// Permet de convertir une date en string au format "yyyy-MM-dd".
        /// Si la date est null, retourne null.
        /// Prend en charge les dates en DateTimeOffset.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateToString(DateTimeOffset? date)
        {
            return date?.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Permet de mettre en forme la chaine de string passée en paramètre pour que la première lettre soit en majuscule et le reste tel quel.
        /// Je l'ai fait pour qu'elle fonctionne par exemple comme la méthode ToTitleCase(). 
        /// </summary>
        /// <param name="chaine"></param>
        /// <returns></returns>
        public static string CapitalizePremiereLettre(this string chaine)
        {
            if (string.IsNullOrEmpty(chaine))
            {
                return chaine;
            }
            return chaine.Substring(0, 1).ToUpper() + chaine.Substring(1);
        }

        /// <summary>
        /// Met à jour les icônes des types d'entretiens selon le thème actuel passé en paramètre.
        /// </summary>
        /// <param name="frameworkElement">La view à rechercher les images</param>
        /// <param name="themeActuel"></param>
        public static void MettreAJourIconeSelonTheme(FrameworkElement frameworkElement, ElementTheme themeActuel)
        {
            List<Image> iconesTheme = frameworkElement
                        .GetDescendantsOfType<Image>()
                        .Where(img => img.Tag != null && img.Tag.ToString() == "ThemeDynamique")
                        .ToList();

            foreach (Image icone in iconesTheme)
            {
                string sourceActuelle = (icone.Source as BitmapImage)?.UriSource.ToString();
                if (themeActuel == ElementTheme.Dark && sourceActuelle.Contains("Clair"))
                {
                    icone.Source = new BitmapImage(new Uri(sourceActuelle.Replace("Clair", "Sombre")));
                }
                else if (themeActuel == ElementTheme.Light && sourceActuelle.Contains("Sombre"))
                {
                    icone.Source = new BitmapImage(new Uri(sourceActuelle.Replace("Sombre", "Clair")));
                }
            }
        }
    }

    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Permet de convertir une liste en ObservableCollection comme le ToList() de LINQ.
        /// J'ai trouvé de l'aide sur https://stackoverflow.com/questions/14638115/observablecollection-doesnt-support-addrange-method-so-i-get-notified-for-each
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }
    }


    public static class ControlExtensions // Cette classe vient directement de https://stackoverflow.com/questions/974598/find-all-controls-in-wpf-window-by-type.
    {
        /// <summary>
        /// Permet de récupérer tous les objets du type spécifié en paramètre dans le contrôle parent.
        /// Je la comprends pas complètement puisque je n'avais jamais utilisé la classe VisualTreeHelper avant. 
        /// Mais suffisamment pour l'utiliser et l'adapter comme je l'ai fait.
        /// Source : <see href="https://stackoverflow.com/questions/974598/find-all-controls-in-wpf-window-by-type"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetDescendantsOfType<T>(this DependencyObject root) where T : DependencyObject
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int childrenCount = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    if (child is T match)
                    {
                        yield return match;
                    }
                    queue.Enqueue(child);
                }
            }
        }
    }
}
