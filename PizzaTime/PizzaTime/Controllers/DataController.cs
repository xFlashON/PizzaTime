using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaTime.ViewModels;

namespace PizzaTime.Controllers
{
    [Produces("application/json")]
    [Route("api/Data/[action]")]
    public class DataController : Controller
    {

        private IDataAccess _dataAccess;
        readonly ILogger _logger;

        public DataController(IDataAccess dataAccess, ILogger<DataController> logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }

        public IActionResult GetMenu()
        {

            var productsList = Mapper.Map<IEnumerable<PizzaViewModel>>(_dataAccess.Pizzas.GetAll());

            var productsPriceList = _dataAccess.PizzaPrices.Get(d => d.Date <= DateTime.Now).
                GroupBy(p => p.Id).Select(s => s.OrderByDescending(t => t.Date).FirstOrDefault()).ToList();

            var ingredientsPriceList = _dataAccess.IngredientPrices.Get(d => d.Date <= DateTime.Now).
                GroupBy(p => p.Id).Select(s => s.OrderByDescending(t => t.Date).FirstOrDefault()).ToList();

            foreach(var product in productsList)
            {
                var currentPizzaPrice = productsPriceList.FirstOrDefault(p => p.Pizza.Id == product.Id);

                if(currentPizzaPrice!=null)
                    product.Price = currentPizzaPrice.Price;

                foreach (var ingredient in product.Ingredients)
                {
                    var currentIngredientPrice = ingredientsPriceList.FirstOrDefault(i => i.Ingredient.Id==ingredient.Id);

                    if (currentIngredientPrice != null)
                        ingredient.Price = currentIngredientPrice.Price;
                }
            }

                           
            return Ok(productsList);
        }


    }
}