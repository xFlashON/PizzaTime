using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Pizza> PizzaSet { get; set; }
        public DbSet<Ingredient> IngredientSet { get; set; }
        public DbSet<Customer> CustomerSet { get; set; }
        public DbSet<PizzaPrice> PizzaPriceSet { get; set; }
        public DbSet<IngredientPrice> IngredientPriceSet { get; set; }
        public DbSet<Order> OrderSet { get; set; }
        public DbSet<OrderRow> OrderRowSet { get; set; }
        public DbSet<OrderRowIngredient> OrderRowIngredientsSet { get; set; }
        public DbSet<PizzaImage> PizzaImageSet { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PizzaIngredient>().HasKey(pi=>new {pi.PizzaId, pi.IngredientId});
            modelBuilder.Entity<PizzaIngredient>().HasOne(p => p.Pizza).WithMany(p => p.Ingredients);

            modelBuilder.Entity<Order>().HasMany(o => o.OrderRows).WithOne(o => o.Order).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderRow>().HasMany(o => o.OrderRowIngredients).WithOne(o => o.OrderRow).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

    }
}
