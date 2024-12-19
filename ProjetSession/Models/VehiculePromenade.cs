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
    /// Classe de véhicule de promenade héritant de véhicule terrestre.
    /// </summary>
    public class VehiculePromenade : VehiculeTerrestre
    {
        public CarrosserieVehiculePromenade Carrosserie { get; set; }
        public bool? EstSportive { get; set; }
        public bool? EstDecapotable { get; set; }
        public bool? AEssuieGlacesArriere { get; set; }
        public bool? ARoueDeSecours { get; set; }

        public VehiculePromenade(string vin = "", string modele = "", Couleurs couleur = Couleurs.Autre, ConstructeurNom marque = ConstructeurNom.Autre, int annee = 1899, DateTimeOffset? dateAchat = null, CarburantType carburant = CarburantType.Essence, double consommation = 0.0, BoiteVitesseType boiteVitesse = BoiteVitesseType.Automatique, TransmissionType transmission = TransmissionType.FWD, int nombreRoues = 4, int nombrePassagers = 5, int nombrePortes = 5, double nombreCylindres = 0, int vitesseMax = 0, int chevauxVapeur = 0, double prixConstructeurActuel = 0.0, double prixConstructeurAchat = 0.0, int kilometrage = 0, int etatMoteur = 100, int etatHuile = 100, int etatTransmission = 100, int etatPneus = 100,CarrosserieVehiculePromenade carrosserie = CarrosserieVehiculePromenade.Berline, bool? estSportive = null, bool? estDecapotable = null, bool? aEssuieGlacesArriere = null, bool? aRoueDeSecours = null) : base(vin, modele, couleur, marque, annee, dateAchat, carburant, consommation, boiteVitesse, transmission, nombreRoues, nombrePassagers, nombrePortes, nombreCylindres, vitesseMax, chevauxVapeur, prixConstructeurActuel, prixConstructeurAchat, kilometrage, etatMoteur, etatHuile, etatTransmission, etatPneus)
        {
            Carrosserie = carrosserie;
            EstSportive = estSportive;
            EstDecapotable = estDecapotable;
            AEssuieGlacesArriere = aEssuieGlacesArriere;
            ARoueDeSecours = aRoueDeSecours;
        }

        public VehiculePromenade() : base()
        {
            Carburant = CarburantType.Essence;
            BoiteVitesse = BoiteVitesseType.Automatique; 
            Transmission = TransmissionType.FWD;
            Annee = 1899;
            DateAchat = DateTimeOffset.Now;
            NombreRoues = 4;
            NombrePassagers = 2;
            NombrePortes = 2;
            PrixConstructeurAchat = 0.0;
        }
    }
}
