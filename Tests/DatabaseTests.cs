using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class DatabaseTests
    {

        [TestMethod]
        public void DatabaseContextCanCreate()
        {

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Test;Trusted_Connection=True;");

            ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options);

            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            Assert.IsNotNull(context);

        }

        [TestMethod]
        public void UnitOfWorkTests()
        {

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Test;Trusted_Connection=True;");

            ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options);

            context.Database.EnsureCreated();

            UnitOfWork unit = new UnitOfWork(context);

            Pizza pizza1 = new Pizza { Name = "Pizza1" };
            Pizza pizza2 = new Pizza { Name = "Pizza2" };

            Ingredient ingredient1 = new Ingredient { Name = "Ingredient 1" };
            Ingredient ingredient2 = new Ingredient { Name = "Ingredient 2" };

            IngredientPrice ingredientPrice = new IngredientPrice { Date = new DateTime(2018, 01, 01), Ingredient = ingredient1, Price = 10 };
            IngredientPrice ingredientPrice2 = new IngredientPrice { Date = new DateTime(2019, 01, 01), Ingredient = ingredient1, Price = 15 };

            PizzaPrice pizzaPrice = new PizzaPrice { Date = new DateTime(2018, 01, 01), Pizza = pizza1, Price = 100 };
            PizzaPrice pizzaPrice2 = new PizzaPrice { Date = new DateTime(2018, 01, 01), Pizza = pizza2, Price = 110 };

            unit.Ingredients.Create(ingredient1);
            unit.Ingredients.Create(ingredient2);

            unit.SaveChanges();

            pizza1.Ingredients.Add(new PizzaIngredient { Pizza = pizza1, Ingredient = ingredient1, IngredientId = ingredient1.Id });
            pizza1.Ingredients.Add(new PizzaIngredient { Pizza = pizza1, Ingredient = ingredient2, IngredientId = ingredient2.Id });

            pizza2.Ingredients.Add(new PizzaIngredient { Pizza = pizza2, Ingredient = ingredient1, IngredientId = ingredient1.Id });

            unit.Pizzas.Create(pizza1);
            unit.Pizzas.Create(pizza2);

            unit.IngredientPrices.Create(ingredientPrice);
            unit.IngredientPrices.Create(ingredientPrice2);

            unit.PizzaPrices.Create(pizzaPrice);
            unit.PizzaPrices.Create(pizzaPrice2);

            unit.SaveChanges();

            var pizzaList = (List<Pizza>)unit.Pizzas.GetAll();

            Assert.AreEqual(pizzaList.Count, 2);
            Assert.AreEqual(pizzaList[0].Ingredients.Count, 2);

        }

    }
}
