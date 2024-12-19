using ProjetSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Data
{
    /// <summary>
    /// Classe qui permet de gérer les entretiens dans la base de données
    /// </summary>
    public class DBEntretiensDataProvider : IEntretiensDataProvider
    {
        /// <summary>
        /// Ajoute un entretien à la base de données
        /// </summary>
        /// <param name="entretien"></param>
        public void AjouterEntretien(Entretiens entretien)
        {
            using DataBaseContext context = new DataBaseContext();
            context.Entretiens.Add(entretien);
            context.SaveChanges();
        }

        /// <summary>
        /// Retourne la liste des entretiens selon le type d'entretien spécifié, si le parametre est null, retourne tous les entretiens
        /// </summary>
        /// <param name="TypeEntretienId"></param>
        /// <returns></returns>
        public List<Entretiens> GetEntretiens(int? TypeEntretienId = null)
        {
            using DataBaseContext context = new DataBaseContext();
            return TypeEntretienId switch
            {
                1 or 2 or 3 or 4 => 
                    context.Entretiens
                        .Where(e => e.TypeEntretien == TypeEntretienId)
                        .OrderByDescending(e => e.Date)
                        .ToList(),

                _ => context.Entretiens
                        .OrderByDescending(e => e.Date)
                        .ToList(),
            };
        }

        /// <summary>
        /// Retourne la liste des entretiens pour le véhicule spécifié en paramètre
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        public List<Entretiens> GetEntretiensPourVehicule(string vin)
        {
            using DataBaseContext context = new DataBaseContext();
            return context.Entretiens.Where(e => e.Vin == vin).ToList();
        }

        /// <summary>
        /// Retire un entretien de la base de données
        /// </summary>
        /// <param name="entretien"></param>
        public void RetirerEntretien(Entretiens entretien)
        {
            using DataBaseContext context = new DataBaseContext();
            context.Entretiens.Remove(entretien);
            context.SaveChanges();
        }
    }
}
