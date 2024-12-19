using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetSession.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entretiens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeEntretien = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Etat = table.Column<int>(type: "INTEGER", nullable: false),
                    Kilometrage = table.Column<int>(type: "INTEGER", nullable: false),
                    Vin = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entretiens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehiculeConstructions",
                columns: table => new
                {
                    Vin = table.Column<string>(type: "TEXT", nullable: false),
                    EstHomologueRoute = table.Column<bool>(type: "INTEGER", nullable: true),
                    PermisSpecial = table.Column<int>(type: "INTEGER", nullable: false),
                    TypeVehicule = table.Column<int>(type: "INTEGER", nullable: false),
                    Modele = table.Column<string>(type: "TEXT", nullable: true),
                    Couleur = table.Column<int>(type: "INTEGER", nullable: false),
                    Marque = table.Column<int>(type: "INTEGER", nullable: false),
                    Annee = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAchat = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Carburant = table.Column<int>(type: "INTEGER", nullable: false),
                    Consommation = table.Column<double>(type: "REAL", nullable: false),
                    BoiteVitesse = table.Column<int>(type: "INTEGER", nullable: false),
                    Transmission = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreRoues = table.Column<int>(type: "INTEGER", nullable: false),
                    NombrePassagers = table.Column<int>(type: "INTEGER", nullable: false),
                    NombrePortes = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreCylindres = table.Column<double>(type: "REAL", nullable: false),
                    VitesseMax = table.Column<int>(type: "INTEGER", nullable: false),
                    ChevauxVapeur = table.Column<int>(type: "INTEGER", nullable: false),
                    PrixConstructeurActuel = table.Column<double>(type: "REAL", nullable: false),
                    PrixConstructeurAchat = table.Column<double>(type: "REAL", nullable: false),
                    Kilometrage = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatMoteur = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatHuile = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatTransmission = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatPneus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculeConstructions", x => x.Vin);
                });

            migrationBuilder.CreateTable(
                name: "VehiculeLourds",
                columns: table => new
                {
                    Vin = table.Column<string>(type: "TEXT", nullable: false),
                    Domaine = table.Column<int>(type: "INTEGER", nullable: false),
                    PoidsMax = table.Column<double>(type: "REAL", nullable: false),
                    ChargementMax = table.Column<double>(type: "REAL", nullable: false),
                    Hauteur = table.Column<double>(type: "REAL", nullable: false),
                    VitesseMaxAutorisee = table.Column<int>(type: "INTEGER", nullable: false),
                    PermisRequis = table.Column<int>(type: "INTEGER", nullable: false),
                    Modele = table.Column<string>(type: "TEXT", nullable: true),
                    Couleur = table.Column<int>(type: "INTEGER", nullable: false),
                    Marque = table.Column<int>(type: "INTEGER", nullable: false),
                    Annee = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAchat = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Carburant = table.Column<int>(type: "INTEGER", nullable: false),
                    Consommation = table.Column<double>(type: "REAL", nullable: false),
                    BoiteVitesse = table.Column<int>(type: "INTEGER", nullable: false),
                    Transmission = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreRoues = table.Column<int>(type: "INTEGER", nullable: false),
                    NombrePassagers = table.Column<int>(type: "INTEGER", nullable: false),
                    NombrePortes = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreCylindres = table.Column<double>(type: "REAL", nullable: false),
                    VitesseMax = table.Column<int>(type: "INTEGER", nullable: false),
                    ChevauxVapeur = table.Column<int>(type: "INTEGER", nullable: false),
                    PrixConstructeurActuel = table.Column<double>(type: "REAL", nullable: false),
                    PrixConstructeurAchat = table.Column<double>(type: "REAL", nullable: false),
                    Kilometrage = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatMoteur = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatHuile = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatTransmission = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatPneus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculeLourds", x => x.Vin);
                });

            migrationBuilder.CreateTable(
                name: "VehiculePromenades",
                columns: table => new
                {
                    Vin = table.Column<string>(type: "TEXT", nullable: false),
                    Carrosserie = table.Column<int>(type: "INTEGER", nullable: false),
                    EstSportive = table.Column<bool>(type: "INTEGER", nullable: true),
                    EstDecapotable = table.Column<bool>(type: "INTEGER", nullable: true),
                    AEssuieGlacesArriere = table.Column<bool>(type: "INTEGER", nullable: true),
                    ARoueDeSecours = table.Column<bool>(type: "INTEGER", nullable: true),
                    Modele = table.Column<string>(type: "TEXT", nullable: true),
                    Couleur = table.Column<int>(type: "INTEGER", nullable: false),
                    Marque = table.Column<int>(type: "INTEGER", nullable: false),
                    Annee = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAchat = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Carburant = table.Column<int>(type: "INTEGER", nullable: false),
                    Consommation = table.Column<double>(type: "REAL", nullable: false),
                    BoiteVitesse = table.Column<int>(type: "INTEGER", nullable: false),
                    Transmission = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreRoues = table.Column<int>(type: "INTEGER", nullable: false),
                    NombrePassagers = table.Column<int>(type: "INTEGER", nullable: false),
                    NombrePortes = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreCylindres = table.Column<double>(type: "REAL", nullable: false),
                    VitesseMax = table.Column<int>(type: "INTEGER", nullable: false),
                    ChevauxVapeur = table.Column<int>(type: "INTEGER", nullable: false),
                    PrixConstructeurActuel = table.Column<double>(type: "REAL", nullable: false),
                    PrixConstructeurAchat = table.Column<double>(type: "REAL", nullable: false),
                    Kilometrage = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatMoteur = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatHuile = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatTransmission = table.Column<int>(type: "INTEGER", nullable: false),
                    EtatPneus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculePromenades", x => x.Vin);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entretiens");

            migrationBuilder.DropTable(
                name: "VehiculeConstructions");

            migrationBuilder.DropTable(
                name: "VehiculeLourds");

            migrationBuilder.DropTable(
                name: "VehiculePromenades");
        }
    }
}
