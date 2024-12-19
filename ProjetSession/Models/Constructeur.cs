using ProjetSession.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Models
{
    /// <summary>
    /// Classe Constructeur (les marques de véhicules)
    /// </summary>
    public class Constructeur
    {
        public ConstructeurNom Nom { get; set; }
        public List<VehiculeTerrestre> Vehicules { get; set; }

        public Constructeur(ConstructeurNom nom=ConstructeurNom.Autre)
        {
            Nom = nom;
            Vehicules = new List<VehiculeTerrestre>();
        }
    }
}
