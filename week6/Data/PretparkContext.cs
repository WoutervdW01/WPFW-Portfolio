using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    public class PretparkContext : DbContext
    {
        public PretparkContext (DbContextOptions<PretparkContext> options)
            : base(options)
        {
        }

        public DbSet<Attractie> Attractie { get; set; } = default!;
    }
