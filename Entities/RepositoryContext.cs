using Entities.DbConfiguration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FridgeConfiguration());
            modelBuilder.ApplyConfiguration(new FridgeModelConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new FridgeProductsConfiguration());
        }


        public DbSet<Fridge> Fridges { get; set; }

        public DbSet<FridgeModel> FridgeModels { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<FridgeProducts> FridgeProducts { get; set; }
    }
}
