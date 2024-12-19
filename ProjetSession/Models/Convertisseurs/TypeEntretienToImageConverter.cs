using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Models.Convertisseurs
{
    /// <summary>
    /// Convertisseur de type d'entretien en chemin d'image pour l'interface.
    /// Si le type d'entretien n'est pas reconnu, l'image par défaut est retournée.
    /// Source des images : https://www.flaticon.com/ (licence gratuite, Nécessite seulement une mention)
    /// </summary>
    public class TypeEntretienToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int typeEntretien)
            {
                return typeEntretien switch
                {
                    1 => "ms-appx:///Assets/Icon-moteur.png",
                    2 => "ms-appx:///Assets/Icon-huile-moteur.png",
                    3 => "ms-appx:///Assets/Icon-transmission.png",
                    4 => "ms-appx:///Assets/Icon-pneus.png",
                    _ => "ms-appx:///Assets/StoreLogo.png"
                };
            }
            return "ms-appx:///Assets/Icon-pneus.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
