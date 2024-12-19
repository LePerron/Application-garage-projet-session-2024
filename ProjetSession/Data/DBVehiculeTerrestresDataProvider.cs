using ProjetSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Data
{
    /// <summary>
    /// Classe qui permet de gérer les véhicules terrestres dans la base de données
    /// </summary>
    public class DBVehiculeTerrestresDataProvider : IVehiculeTerrestreDataProvider
    {
        /// <summary>
        /// Ajoute un véhicule terrestre dans la base de données
        /// </summary>
        /// <param name="vehiculeTerrestre"></param>
        public void AjoutVehiculeTerrestre(VehiculeTerrestre vehiculeTerrestre)
        {
            using DataBaseContext context = new DataBaseContext();
            switch (vehiculeTerrestre)
            {
                case VehiculePromenade vehiculePromenade:
                    context.VehiculePromenades.Add(vehiculePromenade);
                    break;
                case VehiculeConstruction vehiculeConstruction:
                    context.VehiculeConstructions.Add(vehiculeConstruction);
                    break;
                case VehiculeLourd vehiculeLourd:
                    context.VehiculeLourds.Add(vehiculeLourd);
                    break;
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Retire un véhicule terrestre de la base de données
        /// </summary>
        /// <param name="vehiculeTerrestre"></param>
        public void RetirerVehiculeTerrestre(VehiculeTerrestre vehiculeTerrestre)
        {
            using DataBaseContext context = new DataBaseContext();
            switch (vehiculeTerrestre)
            {
                case VehiculePromenade vehiculePromenade:
                    context.VehiculePromenades.Remove(vehiculePromenade);
                    break;
                case VehiculeConstruction vehiculeConstruction:
                    context.VehiculeConstructions.Remove(vehiculeConstruction);
                    break;
                case VehiculeLourd vehiculeLourd:
                    context.VehiculeLourds.Remove(vehiculeLourd);
                    break;
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Modifie un véhicule terrestre dans la base de données
        /// </summary>
        /// <param name="vehiculeTerrestre"></param>
        public void ModifierVehiculeTerrestre(VehiculeTerrestre vehiculeTerrestre)
        {
            using DataBaseContext context = new DataBaseContext();
            switch (vehiculeTerrestre)
            {
                case VehiculePromenade vehiculePromenade:
                    context.VehiculePromenades.Update(vehiculePromenade);
                    break;
                case VehiculeConstruction vehiculeConstruction:
                    context.VehiculeConstructions.Update(vehiculeConstruction);
                    break;
                case VehiculeLourd vehiculeLourd:
                    context.VehiculeLourds.Update(vehiculeLourd);
                    break;
            }
            context.SaveChanges();
        }


        /// <summary>
        /// Retourne la liste des véhicules terrestres, promenade, construction et lourds de la base de données
        /// </summary>
        /// <returns></returns>
        public List<VehiculeTerrestre> GetVehiculesTerrestres()
        {
            List<VehiculeTerrestre> vehiculeTerrestres = new List<VehiculeTerrestre>();
            using DataBaseContext context = new DataBaseContext();

            List<VehiculePromenade> vehiculePromenades =
                context.VehiculePromenades
                .OrderBy(v => v.Marque)
                .ThenByDescending(v => v.Modele)
                .ToList();

            List<VehiculeConstruction> vehiculeConstructions =
                context.VehiculeConstructions
                .OrderBy(v => v.Marque)
                .ThenByDescending(v => v.Modele)
                .ToList();

            List<VehiculeLourd> vehiculeLourds =
                context.VehiculeLourds
                .OrderBy(v => v.Marque)
                .ThenByDescending(v => v.Modele)
                .ToList();

            vehiculeTerrestres.AddRange(vehiculePromenades);
            vehiculeTerrestres.AddRange(vehiculeConstructions);
            vehiculeTerrestres.AddRange(vehiculeLourds);
            return vehiculeTerrestres;
        }
    }
}
