using Microsoft.EntityFrameworkCore;
using pretpark.database.models;
using pretpark.database.administratie;

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
    public DbSet<GastInfo> gastInfo{get; set;}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Gebruiker>().ToTable("Gebruikers");
        builder.Entity<Gast>().ToTable("Gasten");
        builder.Entity<Medewerker>().ToTable("Medewerkers");

        builder.Entity<Gast>()
                .HasOne(g => g.Begeleider)
                .WithOne(g => g.Begeleidt)
                .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<GastInfo>().OwnsOne(gi => gi.coordinate);

        builder.Entity<GastInfo>()
                .HasKey(gi => gi.Id);

        builder.Entity<GastInfo>()
                .HasOne(gi => gi.gast)
                .WithOne(g => g.gastInfo)
                .HasForeignKey<Gast>(g => g.gastInfoId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server = HP-WOUTER;Initial Catalog=YourDatabase;Integrated Security=true");
    }

}

public class MainClass
{
    private static async Task<T> Willekeurig<T>(DbContext c) where T : class => await c.Set<T>().OrderBy(r => EF.Functions.Random()).FirstAsync();
    public static async Task Main(string[] args)
    {
        Random random = new Random(1);
        using (DatabaseContext c = new DatabaseContext())
        {
            c.attracties.RemoveRange(c.attracties);
            c.gebruikers.RemoveRange(c.gebruikers);
            c.gasten.RemoveRange(c.gasten);
            c.medewerkers.RemoveRange(c.medewerkers);
            c.reservering.RemoveRange(c.reservering);
            c.ondehoud.RemoveRange(c.ondehoud);

            c.SaveChanges();

            foreach (string attractie in new string[] { "Reuzenrad", "Spookhuis", "Achtbaan 1", "Achtbaan 2", "Draaimolen 1", "Draaimolen 2" })
                c.attracties.Add(new Attractie(attractie));

            c.SaveChanges();

            for (int i=0;i<40;i++)
                c.medewerkers.Add(new Medewerker($"medewerker{i}@mail.com"));
            c.SaveChanges();

            for (int i = 0; i < 10000; i++)
            {
                var geboren = DateTime.Now.AddDays(-random.Next(36500));
                var nieuweGast = new Gast($"gast{i}@mail.com") { GeboorteDatum = geboren, EersteBezoek = geboren + (DateTime.Now - geboren) * random.NextDouble(), Credits = random.Next(5) };
                if (random.NextDouble() > .6)
                    nieuweGast.Favoriet = await Willekeurig<Attractie>(c);
                c.gasten.Add(nieuweGast);
            }
            c.SaveChanges();

            for (int i = 0; i < 10; i++)
                (await Willekeurig<Gast>(c)).Begeleider = await Willekeurig<Gast>(c);
            c.SaveChanges();


            Console.WriteLine("Finished initialization");

            Console.Write(await new DemografischRapport(c).Genereer());
            Console.ReadLine();
        }
    }
}