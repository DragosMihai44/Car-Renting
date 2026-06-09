using Microsoft.EntityFrameworkCore;
using RentCar.Models;

namespace RentCar.Data;

public class RentCarDbContext : DbContext
{
    public DbSet<Utilizator>      Utilizatori      { get; set; }
    public DbSet<CategorieVehicul> CategoriiVehicule { get; set; }
    public DbSet<Vehicul>         Vehicule          { get; set; }
    public DbSet<Client>          Clienti           { get; set; }
    public DbSet<Rezervare>       Rezervari         { get; set; }
    public DbSet<Contract>        Contracte         { get; set; }
    public DbSet<Returnare>       Returnari         { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=rentcar.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Rezervare>()
            .HasOne(r => r.Client)
            .WithMany(c => c.Rezervari)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

    
        modelBuilder.Entity<Rezervare>()
            .HasOne(r => r.Vehicul)
            .WithMany(v => v.Rezervari)
            .HasForeignKey(r => r.VehiculId)
            .OnDelete(DeleteBehavior.Restrict);

       
        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Rezervare)
            .WithOne(r => r.Contract)
            .HasForeignKey<Contract>(c => c.RezervareId);

        
        modelBuilder.Entity<Returnare>()
            .HasOne(ret => ret.Rezervare)
            .WithOne(r => r.Returnare)
            .HasForeignKey<Returnare>(ret => ret.RezervareId);

      
        modelBuilder.Entity<Utilizator>().HasData(
            new Utilizator { Id = 1, NumeUtilizator = "admin",   ParolaHash = Utilizator.HashParola("admin123"),   Rol = RolUtilizator.Administrator, Activ = true },
            new Utilizator { Id = 2, NumeUtilizator = "agent",   ParolaHash = Utilizator.HashParola("agent123"),   Rol = RolUtilizator.Agent,          Activ = true },
            new Utilizator { Id = 3, NumeUtilizator = "mecanic", ParolaHash = Utilizator.HashParola("mecanic123"), Rol = RolUtilizator.Mecanic,        Activ = true }
        );

        modelBuilder.Entity<CategorieVehicul>().HasData(
            new CategorieVehicul { Id = 1, Denumire = "Mica",       TarifMinim = 80,  TarifMaxim = 120 },
            new CategorieVehicul { Id = 2, Denumire = "Compacta",   TarifMinim = 120, TarifMaxim = 180 },
            new CategorieVehicul { Id = 3, Denumire = "SUV",        TarifMinim = 200, TarifMaxim = 350 },
            new CategorieVehicul { Id = 4, Denumire = "Premium",    TarifMinim = 350, TarifMaxim = 600 },
            new CategorieVehicul { Id = 5, Denumire = "Utilitara",  TarifMinim = 150, TarifMaxim = 250 }
        );
    }
}
