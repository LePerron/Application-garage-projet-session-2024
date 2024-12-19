using Microsoft.EntityFrameworkCore.Migrations;
using ProjetSession.Models.Types;
using System;

#nullable disable

namespace ProjetSession.Migrations
{
    /// <inheritdoc />
    public partial class SeedingVehiculesInitiaux : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "VehiculeConstructions",
            columns: new[] { "Vin", "Modele", "Couleur", "Marque", "Annee", "DateAchat", "Carburant", "Consommation", "BoiteVitesse", "Transmission", "NombreCylindres", "VitesseMax", "ChevauxVapeur", "NombreRoues", "NombrePassagers", "NombrePortes", "PrixConstructeurActuel", "PrixConstructeurAchat", "Kilometrage", "EtatMoteur", "EtatHuile", "EtatTransmission", "EtatPneus", "EstHomologueRoute", "PermisSpecial", "TypeVehicule" },
            values: new object[,]
            {
                {"1FSDDASDSAS14245", "D7 - Tier 4/Stage V", (int)Couleurs.Jaune, (int)ConstructeurNom.Caterpillar, 2023, new DateTime(2023, 1, 23), (int)CarburantType.Diesel, 20.5, (int)BoiteVitesseType.Automatique, (int)TransmissionType.AWD, 6, 50, 450, 4, 1, 1, 50000, 25000, 23254, 53, 25, 61, 23, true, (int)PermisType.Classe4B, (int)ConstructionVehiculeType.Bulldozer}
            });

            migrationBuilder.InsertData(
            table: "VehiculePromenades",
            columns: new[] { "Vin", "Modele", "Couleur", "Marque", "Annee", "DateAchat", "Carburant", "Consommation", "BoiteVitesse", "Transmission", "NombreCylindres", "VitesseMax", "ChevauxVapeur", "NombreRoues", "NombrePassagers", "NombrePortes", "PrixConstructeurActuel", "PrixConstructeurAchat", "Kilometrage", "EtatMoteur", "EtatHuile", "EtatTransmission", "EtatPneus", "Carrosserie", "EstSportive", "EstDecapotable", "AEssuieGlacesArriere", "ARoueDeSecours" },
            values: new object[,]
            {
                {"1FAVDASDSAS12345", "F-150", (int)Couleurs.Blanc, (int)ConstructeurNom.Ford, 2020, new DateTime(2021, 1, 1), (int)CarburantType.Essence, 12.5, (int)BoiteVitesseType.Automatique, (int)TransmissionType.AWD, 8, 200, 450, 4, 5, 4, 50000, 20000, 125982, 52, 63, 12, 9, (int)CarrosserieVehiculePromenade.Utilitaire, false, false, false, true },
                {"2FAJGASDSAS13246", "Civic", (int)Couleurs.Bleu, (int)ConstructeurNom.Honda, 2012, new DateTime(2013, 1, 1), (int)CarburantType.Essence, 7.5, (int)BoiteVitesseType.Manuelle, (int)TransmissionType.FWD, 4, 170, 450, 4, 5, 5, 25000, 26000, 18952, 42, 53, 33, 42, (int)CarrosserieVehiculePromenade.Compacte, true, false, false, true }
            });

            migrationBuilder.InsertData(
            table: "VehiculeLourds",
            columns: new[] { "Vin", "Modele", "Couleur", "Marque", "Annee", "DateAchat", "Carburant", "Consommation", "BoiteVitesse", "Transmission", "NombreCylindres", "VitesseMax", "ChevauxVapeur", "NombreRoues", "NombrePassagers", "NombrePortes", "PrixConstructeurActuel", "PrixConstructeurAchat", "Kilometrage", "EtatMoteur", "EtatHuile", "EtatTransmission", "EtatPneus", "Domaine", "PoidsMax", "ChargementMax", "Hauteur", "VitesseMaxAutorisee", "PermisRequis" },
            values: new object[,]
            {
                {"5CEDFJLIJKG45312", "T680", (int)Couleurs.Rouge, (int)ConstructeurNom.Kenworth, 2023, new DateTime(2023, 2, 1), (int)CarburantType.Diesel, 30, (int)BoiteVitesseType.Manuelle, (int)TransmissionType.AWD, 6, 140, 605, 6, 2, 2, 200000, 2190000, 321653, 63, 61, 92, 83, (int)DomaineType.Transportation, 36287, 13600, 4.1, 110, (int)PermisType.Classe1 },
                {"9ASRFASFARG32351", "T880", (int)Couleurs.Bleu, (int)ConstructeurNom.Kenworth, 2019, new DateTime(2019, 2, 1), (int)CarburantType.Diesel, 23, (int)BoiteVitesseType.Manuelle, (int)TransmissionType.FWD, 6, 140, 605, 4, 5, 6, 900000, 1229999, 345232, 32, 92, 70, 72, (int)DomaineType.Transportation, 36287, 13600, 4.1, 110, (int)PermisType.Classe1 }

            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM VehiculeConstructions", true);
            migrationBuilder.Sql("DELETE FROM VehiculePromenades", true);
            migrationBuilder.Sql("DELETE FROM VehiculeLourds", true);
        }
    }
}
