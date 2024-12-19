using Microsoft.EntityFrameworkCore;
using ProjetSession.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Models
{
    /// <summary>
    /// Classe de véhicule lourd héritant de véhicule terrestre.
    /// </summary>
    public class VehiculeLourd : VehiculeTerrestre
    {
        public DomaineType Domaine { get; set; }
        public double PoidsMax { get; set; }
        public double ChargementMax { get; set; }
        public double Hauteur { get; set; }
        public int VitesseMaxAutorisee { get; set; }
        public PermisType PermisRequis { get; set; }

        public VehiculeLourd(string vin = "", string modele = "", Couleurs couleur = Couleurs.Autre, ConstructeurNom marque = ConstructeurNom.Autre, int annee = 1899, DateTimeOffset? dateAchat = null, CarburantType carburant = CarburantType.Diesel, double consommation = 0.0, BoiteVitesseType boiteVitesse = BoiteVitesseType.Manuelle, TransmissionType transmission = TransmissionType.AWD, double nombreCylindres = 0, int vitesseMax = 0, int chevauxVapeur = 0, int nombreRoues = 4, int nombrePassagers = 2, int nombrePortes = 2, double prixConstructeurActuel = 0.0, double prixConstructeurAchat = 0.0, int kilometrage = 0, int etatMoteur = 100, int etatHuile = 100,  int etatTransmission = 100, int etatPneus = 100, DomaineType domaine = DomaineType.Autre, double poidsMax = 0.0, double chargementMax = 0.0, double hauteur = 0.0, int vitesseMaxAutorisee = 0, PermisType permisRequis = PermisType.Classe1) : base(vin, modele, couleur, marque, annee, dateAchat, carburant, consommation, boiteVitesse, transmission, nombreRoues, nombrePassagers, nombrePortes, nombreCylindres, vitesseMax, chevauxVapeur, prixConstructeurActuel, prixConstructeurAchat, kilometrage, etatMoteur, etatHuile, etatTransmission, etatPneus)
        {
            Domaine = domaine;
            PoidsMax = poidsMax;
            ChargementMax = chargementMax;
            Hauteur = hauteur;
            VitesseMaxAutorisee = vitesseMaxAutorisee;
            PermisRequis = permisRequis;
        }

        public VehiculeLourd() : base()
        {
            Carburant = CarburantType.Diesel;
            BoiteVitesse = BoiteVitesseType.Manuelle;
            Transmission = TransmissionType.AWD;
            Annee = 1899;
            DateAchat = DateTimeOffset.Now;
            NombreRoues = 4;
            NombrePassagers = 2;
            NombrePortes = 2;
            PrixConstructeurAchat = 0.0;
            PermisRequis = PermisType.Classe1;
        }
    }
}
