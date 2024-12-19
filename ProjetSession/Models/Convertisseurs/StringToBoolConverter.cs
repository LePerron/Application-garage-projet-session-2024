using Microsoft.UI.Xaml.Data;
using ProjetSession.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Models.Convertisseurs
{
    // Je me suis servi d'un exemple en ligne pour comprendre comment ça fonctionnait, et donc ce converteur est pas mal ressemblant., source : https://www.c-sharpcorner.com/article/converters-in-wpf/
    /// <summary>
    /// Convertisseur de booléen en string et vice-versa pour l'interface
    /// </summary>
    public partial class StringToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Convertit un booléen en string pour l'interface.
        /// Si le booléen est vrai, retourne "Oui", sinon retourne "Non"
        /// Sinon retourne une chaine vide
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Oui, Non, Sinon Chaine Vide </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool boolean)
            {
                return boolean ? "Oui" : "Non";
            }
            return "";
        }

        /// <summary>
        /// Convertit une chaine en booléen pour l'interface.
        /// Si la chaine est "Oui", retourne vrai, sinon retourne faux
        /// Sinon retourne null
        /// </summary>
        /// <param name="value"></param>
        /// <returns>true, false, Sinon null</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!string.IsNullOrEmpty(value?.ToString()))
            {
                string strValue = value.ToString().ToLower();
                if (strValue == "oui")
                {
                    return true;
                }
                else if (strValue == "non")
                {
                    return false;
                }
            }
            return null;
        }
    }
}
