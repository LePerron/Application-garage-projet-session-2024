using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using ProjetSession.Models.Convertisseurs;
using ProjetSession.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageTests
{
    [TestClass]
    public class StringToBoolConverterTests
    {
        private StringToBoolConverter _converter;

        [TestInitialize]
        public void Initialisation()
        {
            _converter = new StringToBoolConverter();
        }

        [TestMethod]
        public void Convertir_StringOui_RetourneTrue()
        {
            string input = "Oui";
            bool resultat = (bool)_converter.ConvertBack(input, null, null, null);
            Assert.IsTrue(resultat);
        }

        [TestMethod]
        public void Convertir_StringNon_RetourneFalse()
        {
            string input = "Non";
            bool resultat = (bool)_converter.ConvertBack(input, null, null, null);
            Assert.IsFalse(resultat);
        }

        [TestMethod]
        public void Convertir_StringVide_RetourneNull()
        {
            string input = "";
            bool? resultat = (bool?)_converter.ConvertBack(input, null, null, null);
            Assert.IsNull(resultat);
        }

        [TestMethod]
        public void Convertir_Null_RetourneStringVide()
        {
            Assert.AreEqual("", _converter.Convert(null, null, null, null));
        }

        [TestMethod]
        public void ConvertirBack_BooleanEstTrue_RetourneOuiEnString()
        {
            bool input = true;
            string resultat = (string)_converter.Convert(input, null, null, null);
            Assert.AreEqual("Oui", resultat);
        }

        [TestMethod]
        public void ConvertirBack_BooleanEstFalse_RetourneNonEnString()
        {
            bool input = false;
            string resultat = (string)_converter.Convert(input, null, null, null);
            Assert.AreEqual("Non", resultat);
        }
    }

    [TestClass]
    public class EntretienToVisibilityConverterTests
    {
        private EntretienToVisibilityConverter _converter;

        [TestInitialize]
        public void Initialisation()
        {
            _converter = new EntretienToVisibilityConverter();
        }

        [TestMethod]
        public void Convertir_EntretienEstNull_RetourneCollapsed()
        {
            bool? input = null;
            Visibility resultat = (Visibility)_converter.Convert(input, null, null, null);
            Assert.AreEqual(Visibility.Collapsed, resultat);
        }

        [TestMethod]
        public void Convertir_EntretienValide_RetourneVisible()
        {
            EntretienViewModel input = new EntretienViewModel(new ProjetSession.Models.Entretiens(1, DateTime.Now, 80, 100000, "1AAADSASDAS1121"));
            Visibility resultat = (Visibility)_converter.Convert(input, null, null, null);
            Assert.AreEqual(Visibility.Visible, resultat);
        }

        [TestMethod]
        public void Convertir_TypeIncorrect_RetourneCollapsed()
        {
            string input = "invalide car string";
            Visibility resultat = (Visibility)_converter.Convert(input, null, null, null);
            Assert.AreEqual(Visibility.Collapsed, resultat);
        }
    }

    [TestClass]
    public class EtatVehiculeToCouleurConverterTests
    {
        private EtatVehiculeToCouleurConverter _converter;

        [TestInitialize]
        public void Initialisation()
        {
            _converter = new EtatVehiculeToCouleurConverter();
        }

        
        [UITestMethod] // Test de UI parce que le converter utilise les couleurs de l'UI
        public void Convertir_EtatBon_RetourneVert()
        {
            // Liste de valeurs d'état parce que UITestMethod ne supporte pas les DataRows
            int[] etats = [75, 76, 80, 100, 101];
            foreach (int etat in etats)
            {
                SolidColorBrush resultat = (SolidColorBrush)_converter.Convert(etat, null, null, null);
                Assert.AreEqual(Colors.Green, resultat.Color);
            }
        }

        [UITestMethod] // Test de UI parce que le converter utilise les couleurs de l'UI
        public void Convertir_EtatMoyen_RetourneJaune()
        {
            // Liste de valeurs d'état parce que UITestMethod ne supporte pas les DataRows
            int[] etats = [50, 51, 74];
            foreach (int etat in etats)
            {
                SolidColorBrush resultat = (SolidColorBrush)_converter.Convert(etat, null, null, null);
                Assert.AreEqual(Colors.Yellow, resultat.Color);
            }
        }

        [UITestMethod] // Test de UI parce que le converter utilise les couleurs de l'UI
        public void Convertir_EtatMauvais_RetourneOrange()
        {
            // Liste de valeurs d'état parce que UITestMethod ne supporte pas les DataRows
            int[] etats = [25, 26, 49];
            foreach (int etat in etats)
            {
                SolidColorBrush resultat = (SolidColorBrush)_converter.Convert(etat, null, null, null);
                Assert.AreEqual(Colors.Orange, resultat.Color);
            }
        }

        [UITestMethod] // Test de UI parce que le converter utilise les couleurs de l'UI
        public void Convertir_EtatTresMauvais_RetourneRouge()
        {
            // Liste de valeurs d'état parce que UITestMethod ne supporte pas les DataRows
            int[] etats = [0, 1, 24, -5];
            foreach (int etat in etats)
            {
                SolidColorBrush resultat = (SolidColorBrush)_converter.Convert(etat, null, null, null);
                Assert.AreEqual(Colors.Red, resultat.Color);
            }
        }

        [UITestMethod] // Test de UI parce que le converter utilise les couleurs de l'UI
        public void Convertir_EtatAutre_RetourneGris()
        {
            SolidColorBrush resultat = (SolidColorBrush)_converter.Convert("", null, null, null);
            Assert.AreEqual(Colors.Gray, resultat.Color);
        }
    }
}
