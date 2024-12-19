using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Models.Convertisseurs
{
    /// <summary>
    /// Convertisseur de kilométrage en format lisible pour l'interface. (ex: 1000 -> 1k km) 
    /// Si le kilométrage est invalide, retourne "N/A".
    /// </summary>
    public class KilometrageToFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double kilometrage)
            {
                switch (kilometrage)
                {
                    case >= 1000000:
                        {
                            double millions = kilometrage / 1000000.0;
                            return $"{millions:N1}M km";
                        }
                    case >= 1000:
                        {
                            double milliers = kilometrage / 1000.0;
                            return $"{milliers:N1}k km";
                        }
                    default:
                        return $"{kilometrage:N0} km";
                }
            }
            return "N/A"; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}