using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using ProjetSession.Data;
using ProjetSession.ViewModels;
using ProjetSession.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageTests
{
    [TestClass]
    public class TestUnitaireAjouterVehiculeUI
    {
        [UITestMethod] 
        public void TestPageAjouterVehicule_ChangerPageDansFormulaireAjout_ChangeVisibilityDesControlsEtTitreDansPagesDuFormulaireAjout()
        {
            // Liste de tuples contenant les données de test parce que UITestMethod ne supporte pas les DataRows
            List<(int, Visibility, Visibility, Visibility, Visibility, Visibility)> listeDonneesTests = new List<(int, Visibility, Visibility, Visibility, Visibility, Visibility)>
            {
                (0, Visibility.Visible, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed),
                (1, Visibility.Visible, Visibility.Visible, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed),
                (2, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed),
                (3, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible)
            };

            AjouterAuGarage ajouterAuGarageView = new AjouterAuGarage();


            foreach ((int numeroPage, Visibility titreVisibility, Visibility comboboxAjoutVisibility, Visibility page1Visibility, Visibility page2Visibility, Visibility page3Visibility) in listeDonneesTests)
            {
                ajouterAuGarageView.ViewModel.Page = numeroPage;
                ajouterAuGarageView.ModifierLaPage();

                TextBlock TitrePageAjoutModification = (TextBlock)ajouterAuGarageView.FindName("TitrePageAjoutModification");
                ComboBox ComboboxPageAjout = (ComboBox)ajouterAuGarageView.FindName("ComboboxPageAjout");
                Grid Page1 = (Grid)ajouterAuGarageView.FindName("Page1");
                Grid Page2 = (Grid)ajouterAuGarageView.FindName("Page2");
                Grid Page3 = (Grid)ajouterAuGarageView.FindName("Page3");

                Assert.AreEqual(TitrePageAjoutModification.Visibility, titreVisibility);
                Assert.AreEqual(ComboboxPageAjout.Visibility, comboboxAjoutVisibility);
                Assert.AreEqual(Page1.Visibility, page1Visibility);
                Assert.AreEqual(Page2.Visibility, page2Visibility);
                Assert.AreEqual(Page3.Visibility, page3Visibility);
            }
        }
    }
}
