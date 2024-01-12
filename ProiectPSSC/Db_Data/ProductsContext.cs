using proiectpssc.Db_Data.Db_DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace proiectpssc.Db_Data
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
        }

        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<ClientDTO> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientDTO>().ToTable("Client").HasKey(s => s.ClientId);
            modelBuilder.Entity<ProductDTO>().ToTable("Product").HasKey(s => s.ProductId);
        }
    }
}
