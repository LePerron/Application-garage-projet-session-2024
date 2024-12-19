using ProjetSession.Models;
using ProjetSession.Models.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession.Data
{
    /// <summary>
    /// Classe de données pour les véhicules terrestres non connectée à une base de données. (Pas persistant)
    /// </summary>
    public class VehiculeTerrestreDataProvider : IVehiculeTerrestreDataProvider
    {
        public void AjoutVehiculeTerrestre(VehiculeTerrestre vehiculeTerrestre)
        {
            throw new NotImplementedException();
        }

        public void RetirerVehiculeTerrestre(VehiculeTerrestre vehiculeTerrestre)
        {
            throw new NotImplementedException();
        }

        public void ModifierVehiculeTerrestre(VehiculeTerrestre vehiculeTerrestre)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retourne une liste de véhicules terrestres.
        /// </summary>
        /// <returns></returns>
        public List<VehiculeTerrestre> GetVehiculesTerrestres()
        {
            return new List<VehiculeTerrestre>
            {
                new VehiculeConstruction
                (
                    vin: "1FSDDASDSAS14245",
                    modele: "D7 - Tier 4/Stage V",
                    couleur: Couleurs.Jaune,
                    marque: ConstructeurNom.Caterpillar,
                    annee: 2023,
                    dateAchat: new DateTime(2023, 1, 23),
                    carburant: CarburantType.Diesel,
                    consommation: 20.5,
                    boiteVitesse: BoiteVitesseType.Automatique,
                    transmission: TransmissionType.AWD,
                    nombreCylindres: 6,
                    vitesseMax: 50,
                    chevauxVapeur: 450,
                    nombreRoues: 4,
                    nombrePassagers: 1,
                    nombrePortes: 1,
                    prixConstructeurActuel: 50000,
                    prixConstructeurAchat: 25000,
                    kilometrage: 10000,
                    estHomologueRoute: true,
                    permisSpecial: PermisType.Classe4B,
                    typeVehicule: ConstructionVehiculeType.Bulldozer
                ),

                new VehiculePromenade
                (
                    vin: "1FAVDASDSAS12345",
                    modele: "F-150",
                    couleur: Couleurs.Blanc,
                    marque: ConstructeurNom.Ford,
                    annee: 2020,
                    dateAchat: new DateTime(2021, 1, 1),
                    carburant: CarburantType.Essence,
                    consommation: 12.5,
                    boiteVitesse: BoiteVitesseType.Automatique,
                    transmission: TransmissionType.AWD,
                    nombreCylindres: 8,
                    vitesseMax: 200,
                    chevauxVapeur: 450,
                    nombreRoues: 4,
                    nombrePassagers: 5,
                    nombrePortes: 4,
                    prixConstructeurActuel: 50000,
                    prixConstructeurAchat: 20000,
                    kilometrage: 10000,
                    carrosserie: CarrosserieVehiculePromenade.Utilitaire,
                    estSportive: false,
                    estDecapotable: false,
                    aEssuieGlacesArriere: false,
                    aRoueDeSecours: true
                    ),

                new VehiculePromenade
                (
                    vin: "2FAJGASDSAS13246",
                    modele: "Civic",
                    couleur: Couleurs.Bleu,
                    marque: ConstructeurNom.Honda,
                    annee: 2012,
                    dateAchat: new DateTime(2013, 1, 1),
                    carburant: CarburantType.Essence,
                    consommation: 7.5,
                    boiteVitesse: BoiteVitesseType.Manuelle,
                    transmission: TransmissionType.FWD,
                    nombreCylindres: 4,
                    vitesseMax: 170,
                    chevauxVapeur: 450,
                    nombreRoues: 4,
                    nombrePassagers: 5,
                    nombrePortes: 5,
                    prixConstructeurActuel: 25000,
                    prixConstructeurAchat: 19000,
                    kilometrage: 160000,
                    carrosserie: CarrosserieVehiculePromenade.Compacte,
                    estSportive: true,
                    estDecapotable: false,
                    aEssuieGlacesArriere: false,
                    aRoueDeSecours: true
                    ),

                new VehiculeLourd
                (
                    vin: "5CEDFJLIJKG45312",
                    modele: "T680",
                    couleur: Couleurs.Rouge,
                    marque: ConstructeurNom.Kenworth,
                    annee: 2024,
                    dateAchat: new DateTime(2010, 2, 1),
                    carburant: CarburantType.Diesel,
                    consommation: 30,
                    boiteVitesse: BoiteVitesseType.Manuelle,
                    transmission: TransmissionType.AWD,
                    nombreCylindres: 6,
                    vitesseMax: 140,
                    chevauxVapeur: 605,
                    nombreRoues: 6,
                    nombrePassagers: 2,
                    nombrePortes: 2,
                    prixConstructeurActuel: 200000,
                    prixConstructeurAchat: 190000,
                    kilometrage: 80000,
                    domaine: DomaineType.Transportation,
                    poidsMax: 36287,
                    chargementMax: 13600,
                    hauteur: 4.1,
                    vitesseMaxAutorisee: 110,
                    permisRequis: PermisType.Classe1
                    ),
            };
        }
    }
}


