using Microsoft.EntityFrameworkCore;
using ProjetSession.Models.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;
using static ProjetSession.Models.Utilities;

namespace ProjetSession.Models
{
    /// <summary>
    /// Classe mère de tous les véhicules terrestres.
    /// </summary>
    public class VehiculeTerrestre
    {
        [Key]
        [Required]
        public string Vin { get; set; }

        public string Modele { get; set; }
        public Couleurs Couleur { get; set; }
        public ConstructeurNom Marque { get; set; }
        public int Annee { get; set; }
        public DateTimeOffset? DateAchat { get; set; }
        public CarburantType Carburant { get; set; }
        public double Consommation { get; set; }
        public BoiteVitesseType BoiteVitesse { get; set; }
        public TransmissionType Transmission { get; set; }
        public int NombreRoues { get; set; }
        public int NombrePassagers { get; set; }
        public int NombrePortes { get; set; }
        public double NombreCylindres { get; set; }
        public int VitesseMax { get; set; }
        public int ChevauxVapeur { get; set; }
        public double PrixConstructeurActuel { get; set; }
        public double PrixConstructeurAchat { get; set; }
        public int Kilometrage { get; set; }
        public int EtatMoteur { get; set; }
        public int EtatHuile { get; set; }
        public int EtatTransmission { get; set; }
        public int EtatPneus { get; set; }

        public static List<VehiculeTerrestre> Vehicules { get; set; } = new List<VehiculeTerrestre>();

        public VehiculeTerrestre(string vin = "", string modele = "", Couleurs couleur = Couleurs.Autre, ConstructeurNom marque = ConstructeurNom.Autre, int annee = 1899, DateTimeOffset? dateAchat = null, CarburantType carburant = CarburantType.Autre, double consommation = 0.0, BoiteVitesseType boiteVitesse = BoiteVitesseType.Autre, TransmissionType transmission = TransmissionType.Autre, int nombreRoues = 4, int nombrePassagers = 5, int nombrePortes = 5, double nombreCylindres = 0, int vitesseMax = 0, int chevauxVapeur = 0, double prixConstructeurActuel = 0.0, double prixConstructeurAchat = 0.0, int kilometrage = 0, int etatMoteur = 100, int etatHuile = 100, int etatTransmission = 100, int etatPneus = 100)
        {
            Vin = vin;
            Modele = modele;
            Couleur = couleur;
            Marque = marque;
            Annee = annee;
            DateAchat = dateAchat ?? DateTimeOffset.Now;
            Carburant = carburant;
            Consommation = consommation;
            BoiteVitesse = boiteVitesse;
            Transmission = transmission;
            NombreRoues = nombreRoues;
            NombrePassagers = nombrePassagers;
            NombrePortes = nombrePortes;
            NombreCylindres = nombreCylindres;
            VitesseMax = vitesseMax;
            ChevauxVapeur = chevauxVapeur;
            PrixConstructeurActuel = prixConstructeurActuel;
            PrixConstructeurAchat = prixConstructeurAchat;
            Kilometrage = kilometrage;
            EtatMoteur = etatMoteur;
            EtatHuile = etatHuile;
            EtatTransmission = etatTransmission;
            EtatPneus = etatPneus;
        }

        public VehiculeTerrestre()
        {
        }

        /// <summary>
        /// Convertit la marque, le modele et la couleur en un nom de fichier valide. (ex: "Ford, F-150, Blanc" -> "ford_f-150_blanc") 
        /// </summary>
        /// <returns>Le chemin vers l'image du véhicule</returns>
        public string ObtenirCheminImage()
        {
            string nomMarque = ConvertirChaineCaracteresToNomFichier(Marque.ToString().ToLower());
            string nomModele = ConvertirChaineCaracteresToNomFichier(Modele.ToLower());
            string nomCouleur = ConvertirChaineCaracteresToNomFichier(Couleur.ToString().ToLower());
            return @$"ms-appx:///Images/{nomMarque}_{nomModele}_{nomCouleur}.jpg";
        }

        /// <summary>
        /// Permet d'obtenir l'état global du véhicule selon le pourcentage moyen de l'état du moteur, de l'huile, de la transmission et des pneus.
        /// </summary>
        /// <returns>L'état global du véhicule selon les pièces du véhicule.</returns>
        public int ObtenirEtatGlobalVehicule()
        {
            return (EtatMoteur + EtatHuile + EtatTransmission + EtatPneus) / 4;
        }

    }
}
