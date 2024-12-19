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
    /// Classe de véhicule de construction héritant de véhicule terrestre.
    /// </summary>
    public class VehiculeConstruction : VehiculeTerrestre
    {
        public bool? EstHomologueRoute { get; set; }
        public PermisType PermisSpecial { get; set; }
        public ConstructionVehiculeType TypeVehicule { get; set; }

        public VehiculeConstruction(string vin = "", string modele = "", Couleurs couleur = Couleurs.Autre, ConstructeurNom marque = ConstructeurNom.Autre, int annee = 1899, DateTimeOffset? dateAchat = null, CarburantType carburant = CarburantType.Diesel, double consommation = 0.0, BoiteVitesseType boiteVitesse = BoiteVitesseType.Autre, TransmissionType transmission = TransmissionType.Autre, double nombreCylindres = 0, int vitesseMax = 0, int chevauxVapeur = 0, int nombreRoues = 4, int nombrePassagers = 5, int nombrePortes = 5, double prixConstructeurActuel = 0.0, double prixConstructeurAchat = 0.0, int kilometrage = 0, int etatMoteur = 100, int etatHuile = 100, int etatTransmission = 100, int etatPneus = 100, bool estHomologueRoute = true, PermisType permisSpecial = PermisType.Classe4B, ConstructionVehiculeType typeVehicule = ConstructionVehiculeType.Autre) : base(vin, modele, couleur, marque, annee, dateAchat, carburant, consommation, boiteVitesse, transmission, nombreRoues, nombrePassagers, nombrePortes, nombreCylindres, vitesseMax, chevauxVapeur, prixConstructeurActuel, prixConstructeurAchat, kilometrage, etatMoteur, etatHuile, etatTransmission, etatPneus)
        {
            EstHomologueRoute = estHomologueRoute;
            PermisSpecial = permisSpecial;
            TypeVehicule = typeVehicule;
        }

        public VehiculeConstruction() : base()
        {
            Carburant = CarburantType.Diesel;
            BoiteVitesse = BoiteVitesseType.Cvt;
            Transmission = TransmissionType.AWD;
            Annee = 1899;
            DateAchat = DateTimeOffset.Now;
            NombreRoues = 4;
            NombrePassagers = 2;
            NombrePortes = 2;
            PrixConstructeurAchat = 0.0;
            PermisSpecial = PermisType.Classe4B;
        }
    }
}
