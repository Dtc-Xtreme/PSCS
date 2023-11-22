using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PSCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSCS.Infrastructure
{
    public class PSCSDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public PSCSDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("db"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>().HasIndex(p => p.Name).IsUnique(true);
            modelBuilder.Entity<Supplier>().HasIndex(p => p.Number).IsUnique(true);
            modelBuilder.Entity<Product>().HasIndex(p => p.Number).IsUnique(true);
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderLine> OrderLines { get; set; }

    }
}
