using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProjetSession.Models;

namespace ProjetSession;

/// <summary>
/// La classe du DataContext de la base de données. 
/// </summary>
public partial class DataBaseContext : DbContext
{
    public DbSet<VehiculePromenade> VehiculePromenades { get; set; }
    public DbSet<VehiculeLourd> VehiculeLourds { get; set; }
    public DbSet<VehiculeConstruction> VehiculeConstructions { get; set; }
    public DbSet<Entretiens> Entretiens { get; set; }

    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\perronserveur\\source\\Repos\\s14-projet-de-session-marc\\ProjetSession\\Database.sqlite")
        .LogTo(delegate (string text) { Debug.WriteLine(text); }, [DbLoggerCategory.Database.Command.Name], Microsoft.Extensions.Logging.LogLevel.Information)
        .EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
