// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DatabaseInitializer(ApplicationDbContext context, ILogger<DatabaseInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {

            if (!await _context.CustomerSet.AnyAsync())
            {
                _logger.LogInformation("Generating inbuilt accounts");


                await CreateUserAsync("admin", "admin", "Inbuilt Administrator", "admin@temp.com", "+1 (123) 000-0000");

                _logger.LogInformation("Inbuilt account generation completed");
            }


            if(!await _context.PizzaSet.AnyAsync())
            {

                Pizza pizza1 = new Pizza { Name = "Pizza1"};
                Pizza pizza2 = new Pizza { Name = "Pizza2" };

                Ingredient ingredient1 = new Ingredient { Name = "Ingredient 1"};
                Ingredient ingredient2 = new Ingredient { Name = "Ingredient 2" };

                IngredientPrice ingredientPrice = new IngredientPrice { Date = new DateTime(2018, 01, 01), Ingredient = ingredient1, Price = 10 };
                IngredientPrice ingredientPrice2 = new IngredientPrice { Date = new DateTime(2019, 01, 01), Ingredient = ingredient1, Price = 15 };

                PizzaPrice pizzaPrice = new PizzaPrice {Date = new DateTime(2018, 01, 01), Pizza = pizza1, Price = 100 };
                PizzaPrice pizzaPrice2 = new PizzaPrice { Date = new DateTime(2018, 01, 01), Pizza = pizza2, Price = 110 };

                _context.IngredientSet.Add(ingredient1);
                _context.IngredientSet.Add(ingredient2);

                _context.SaveChanges();

                pizza1.Ingredients.Add(new PizzaIngredient { Pizza = pizza1, Ingredient = ingredient1, IngredientId=ingredient1.Id });
                pizza1.Ingredients.Add(new PizzaIngredient { Pizza = pizza1, Ingredient = ingredient2, IngredientId = ingredient2.Id });

                pizza2.Ingredients.Add(new PizzaIngredient { Pizza = pizza2, Ingredient = ingredient1, IngredientId = ingredient1.Id });

                _context.PizzaSet.Add(pizza1);
                _context.PizzaSet.Add(pizza2);

                _context.IngredientPriceSet.Add(ingredientPrice);
                _context.IngredientPriceSet.Add(ingredientPrice2);

                _context.PizzaPriceSet.Add(pizzaPrice);
                _context.PizzaPriceSet.Add(pizzaPrice2);

                _context.SaveChanges();

            }

        }

        private async Task<Customer> CreateUserAsync(string userName, string password, string fullName, string email, string phoneNumber, bool isAdmin = false)
        {
            Customer applicationUser = new Customer
            {
                Name = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                Role = isAdmin ? "Admin" : "User",
                DeliveryAdress = "",
                PasswordHash = "ABgCyOV0FFLNm2MjiAtoPOlX+jMOIGymbS1kS00aTJ5P4TtnViwGeo993Z2+xjvuqw==" //admin
            };

            var result = _context.CustomerSet.Add(applicationUser);

            await _context.SaveChangesAsync();

            return applicationUser;
        }
    }
}
