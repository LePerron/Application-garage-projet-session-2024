using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Models.Convertisseurs
{
    /// <summary>
    /// Convertisseur qui permet de convertir l'état d'un véhicule en couleur pour l'interface. 
    /// Si l'état est invalide, la couleur sera grise.
    /// </summary>
    public class EtatVehiculeToCouleurConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int etat)
            {
                switch (etat)
                {
                    case >= 75:
                        return new SolidColorBrush(Colors.Green);
                    case >= 50:
                        return new SolidColorBrush(Colors.Yellow);
                    case >= 25:
                        return new SolidColorBrush(Colors.Orange);
                    case < 25:
                        return new SolidColorBrush(Colors.Red);
                }
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}