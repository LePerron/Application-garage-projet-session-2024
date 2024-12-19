using ProjetSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Data
{
    /// <summary>
    /// Interface pour les méthodes de gestion des entretiens
    /// </summary>
    public interface IEntretiensDataProvider
    {
        List<Entretiens> GetEntretiens(int? TypeEntretienId=null);
        List<Entretiens> GetEntretiensPourVehicule(string vin);
        void AjouterEntretien(Entretiens entretien);
        void RetirerEntretien(Entretiens entretien);
    }
}
