using Microsoft.EntityFrameworkCore;
using pretpark.database.models;

namespace pretpark.database;
class DatabaseContext : DbContext
{
    public DbSet<Gebruiker> gebruikers {get; set;}
    public DbSet<Gast> gasten {get; set;}
    public DbSet<Medewerker> medewerkers {get; set;}
    public DbSet<DateTimeBereik> dateTimeBereik{get; set;}
    public DbSet<Attractie> attracties {get; set;}
    public DbSet<Onderhoud> ondehoud{get; set;}
    public DbSet<Reservering> reservering{get; set;}
    public DbSet<Coordinate> coordinaat{get; set;}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Gebruiker>().ToTable("Gebruikers");
        builder.Entity<Gast>().ToTable("Gasten");
        builder.Entity<Medewerker>().ToTable("Medewerkers");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server = HP-WOUTER;Initial Catalog=YourDatabase;Integrated Security=true");
    }

    public static void Main(string[] args)
    {

    }
}