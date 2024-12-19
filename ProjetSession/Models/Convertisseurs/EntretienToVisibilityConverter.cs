using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using ProjetSession.ViewModels;
using System;

namespace ProjetSession.Models.Convertisseurs
{
    /// <summary>
    /// Convertisseur permettant de déterminer la visibilité d'un entretien dans l'interface. 
    /// Si l'entretien est null, la visibilité est collapsed, sinon visible.
    /// </summary>
    public class EntretienToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is EntretienViewModel entretien)
            {
                return entretien != null ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}