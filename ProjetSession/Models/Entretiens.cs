using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetSession.Models
{
    /// <summary>
    /// Classe Entretiens qui contient les propriétés de la table Entretiens dans la base de données
    /// </summary>
    public class Entretiens
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; } // L'option DatabaseGenerated permet de générer automatiquement l'identifiant. (Parce que sinon j'avais une erreur de clé primaire)
        public int TypeEntretien { get; set; }
        public DateTime Date { get; set; }
        public int Etat { get; set; }
        public int Kilometrage { get; set; }
        public string Vin { get; set; }

        public Entretiens(int typeEntretien, DateTime date, int etat, int kilometrage, string vin)
        {
            TypeEntretien = typeEntretien;
            Date = date;
            Etat = etat;
            Kilometrage = kilometrage;
            Vin = vin;
        }
    }
}
