using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace warehouse
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Tovar> TovarDB { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)

        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tovar>().HasKey(u => u.Id);
        }
    }
}
