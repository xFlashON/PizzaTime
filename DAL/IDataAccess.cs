using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDataAccess
    {
        IRepository<Pizza> Pizzas { get; }
        IRepository<PizzaImage> PizzaImages { get; }
        IRepository<Ingredient> Ingredients { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Order> Orders { get; }
        IRepository<PizzaPrice> PizzaPrices { get; }
        IRepository<IngredientPrice> IngredientPrices { get; }
        int SaveChanges();

    }
}
