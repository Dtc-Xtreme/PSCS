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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("db"));
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderLine> OrderLines { get; set; }

    }
}
