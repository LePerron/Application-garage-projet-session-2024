using ProjetSession.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Data
{
    /// <summary>
    /// Interface pour les méthodes de gestion des véhicules terrestres
    /// </summary>
    public interface IVehiculeTerrestreDataProvider
    {
        List<VehiculeTerrestre> GetVehiculesTerrestres();
        void AjoutVehiculeTerrestre(VehiculeTerrestre vehiculeTerrestre);
        void RetirerVehiculeTerrestre(VehiculeTerrestre vehiculeTerrestre);
        void ModifierVehiculeTerrestre(VehiculeTerrestre vehiculeTerrestre);
    }
}
