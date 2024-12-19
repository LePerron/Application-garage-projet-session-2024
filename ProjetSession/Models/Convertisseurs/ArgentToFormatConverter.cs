using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;
using ProjetSession.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProjetSession.Models.Convertisseurs
{
    /// <summary>
    /// Convertisseur de double à string pour afficher un montant d'argent avec un format spécifique (ex: 1000.00 -> 1 000 $).
    /// Si le montant est invalide, retourne "N/A"
    /// </summary>
    public class ArgentToFormatConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double argent)
            {
                return string.Format("{0:N0} $", argent);
            }
            return "N/A";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}