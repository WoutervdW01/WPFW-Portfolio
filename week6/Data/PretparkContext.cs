using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class PretparkContext : IdentityDbContext<GebruikerMetGeslacht>
    {
        public PretparkContext (DbContextOptions<PretparkContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Geslacht>().HasData(new Geslacht(1, "Man"));
            modelBuilder.Entity<Geslacht>().HasData(new Geslacht(2, "Vrouw"));
            modelBuilder.Entity<Geslacht>().HasData(new Geslacht(3, "Geheim"));
            modelBuilder.Entity<Geslacht>().HasData(new Geslacht(4, "Anders"));

            modelBuilder.Entity<LikedBy>().HasKey(l => new {l.GastId, l.AttractieId});
            modelBuilder.Entity<LikedBy>()
                        .HasOne(l => l.attractie)
                        .WithMany(a => a.LikedBy)
                        .HasForeignKey(l => l.AttractieId);
            modelBuilder.Entity<LikedBy>()
                        .HasOne(l => l.Gast)
                        .WithMany(g => g.likes)
                        .HasForeignKey(l => l.GastId);
            
        }

        public DbSet<Attractie> Attractie { get; set; } = default!;
        //public DbSet<GebruikerMetGeslacht> GebruikerMetGeslacht {get; set;} = default!;
        public DbSet<Geslacht> Geslacht {get; set; } = default!;
    }
