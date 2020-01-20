using Microsoft.EntityFrameworkCore;
using Products.Data.Models;
using System;

namespace DataAccess
{
    public class ProductContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = products.db");
        }


        public DbSet<ProductOption> ProductOption { get; set; }

        public DbSet<Product> Product { get; set; }


    }
}
