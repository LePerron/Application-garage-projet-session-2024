using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using ProjetSession.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageTests
{
    [TestClass]
    public class TestUnitaireHistoriqueEntretiens
    {

        [UITestMethod]
        public void TestAutoSuggestBox_RechercherEtSelectionnerUnVin_MetVehiculeSelectioneeEtAfficheLesEntretiensAssocies()
        {
            // Liste de tuples contenant les données de test parce que UITestMethod ne supporte pas les DataRows
            List<(string, bool)> donneesDeTests = new List<(string vin, bool estValide)>
            {
                ("1FSDDASDSAS14245", true),
                ("2FAJGASDSAS13246", true),
                ("Test Invalide", false)
            };

            HistoriqueEntretiens historiqueEntretiensView = new HistoriqueEntretiens();
            AutoSuggestBox autoSuggestBox = (AutoSuggestBox)historiqueEntretiensView.FindName("VehicleSelecteur");

            autoSuggestBox.SuggestionChosen += historiqueEntretiensView.VehicleSelecteur_SuggestionChosen;
            autoSuggestBox.TextChanged += historiqueEntretiensView.VehicleSelecteur_TextChanged;

            foreach ((string vin, bool estValide) in donneesDeTests)
            {
                autoSuggestBox.Text = vin;
                historiqueEntretiensView.ViewModel.VehiculeSelectionnee = historiqueEntretiensView.ViewModel.ListeVehicules.FirstOrDefault(v => v.Vin == vin);

                if (estValide)
                {

                    Assert.AreEqual(vin, historiqueEntretiensView.ViewModel.VehiculeSelectionnee?.Vin);
                    Assert.IsTrue(historiqueEntretiensView.ViewModel.ListeEntretiensMoteur.All(e => e.Vin == vin));
                    Assert.IsTrue(historiqueEntretiensView.ViewModel.ListeEntretiensHuile.All(e => e.Vin == vin));
                    Assert.IsTrue(historiqueEntretiensView.ViewModel.ListeEntretiensTransmission.All(e => e.Vin == vin));
                    Assert.IsTrue(historiqueEntretiensView.ViewModel.ListeEntretiensPneus.All(e => e.Vin == vin));
                }
                else
                {
                    Assert.IsNull(historiqueEntretiensView.ViewModel.VehiculeSelectionnee);
                }
            }
        }
    }
}
