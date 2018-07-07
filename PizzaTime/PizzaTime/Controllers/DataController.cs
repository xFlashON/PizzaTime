using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaTime.Helpers;
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

        [HttpGet]
        public IActionResult GetMenu()
        {

            var productsList = Mapper.Map<ICollection<PizzaViewModel>>(_dataAccess.Pizzas.GetAll());

            var productsPriceList = _dataAccess.PizzaPrices.Get(d => d.Date <= DateTime.Now).
                GroupBy(p => p.Id).Select(s => s.OrderByDescending(t => t.Date).FirstOrDefault()).ToList();

            var ingredientsPriceList = _dataAccess.IngredientPrices.Get(d => d.Date <= DateTime.Now).
                GroupBy(p => p.Id).Select(s => s.OrderByDescending(t => t.Date).FirstOrDefault()).ToList();

            foreach (var product in productsList)
            {
                var currentPizzaPrice = productsPriceList.FirstOrDefault(p => p.Pizza.Id == product.Id);

                if (currentPizzaPrice != null)
                    product.Price = currentPizzaPrice.Price;

                foreach (var ingredient in product.Ingredients)
                {
                    var currentIngredientPrice = ingredientsPriceList.FirstOrDefault(i => i.Ingredient.Id == ingredient.Id);

                    if (currentIngredientPrice != null)
                        ingredient.Price = currentIngredientPrice.Price;
                }

                product.ImageUrl = $"api/data/getImage/{product.Id}";

            }


            return Ok(productsList);
        }

        [HttpGet]
        public IActionResult GetIngredients()
        {

            var ingredientList = Mapper.Map<ICollection<IngredientViewModel>>(_dataAccess.Ingredients.GetAll());

            var ingredientsPriceList = _dataAccess.IngredientPrices.Get(d => d.Date <= DateTime.Now).
                GroupBy(p => p.Id).Select(s => s.OrderByDescending(t => t.Date).FirstOrDefault()).ToList();

            foreach (var ingredient in ingredientList)
            {
                var currentIngredientPrice = ingredientsPriceList.FirstOrDefault(i => i.Ingredient.Id == ingredient.Id);

                if (currentIngredientPrice != null)
                    ingredient.Price = currentIngredientPrice.Price;
            }

            return Ok(ingredientList);

        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 10)]
        public IActionResult GetImage(string id)
        {

            var image = _dataAccess.PizzaImages.Get(i=>i.PizzaID.ToString() == id).FirstOrDefault();

            if (image is null)
                return File("/PlaceholderPizza.jpg", "image/jpg");

            return File(image.ImageData, image.MimeType);

        }

        [HttpPost, Authorize]
        public IActionResult SaveOrder([FromBody] OrderViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest("Validation error!");

            Order order = ConverterHelper.ConvertViewModelToOrder(model);

            _dataAccess.Orders.Create(order);

            _dataAccess.SaveChanges();

            return Ok(order.Number);

        }

        #region ManageRegion

        [HttpPost, Authorize(Roles = ("Admin"))]
        public IActionResult SavePizza([FromBody] PizzaViewModel pizza)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pizza.Id == Guid.Empty)
                _dataAccess.Pizzas.Create(Mapper.Map<Pizza>(pizza));
            else
                _dataAccess.Pizzas.Update(Mapper.Map<Pizza>(pizza));

            _dataAccess.SaveChanges();

            return Ok(pizza);

        }


        [HttpPost]
        public IActionResult SaveImage([FromForm] string id, [FromForm] string type, [FromForm] string imagedata)
        {
            if (imagedata is null || id is null)
                return BadRequest();

            Byte[] data = Convert.FromBase64String(imagedata);

            var dbItem = _dataAccess.PizzaImages.Get(i => i.Pizza.Id.ToString() == id).FirstOrDefault();

            if(dbItem is null)
            {
                _dataAccess.PizzaImages.Create(new PizzaImage() { PizzaID = new Guid(id), MimeType = type, ImageData = data });
            }
            else
            {
                _dataAccess.PizzaImages.Update(new PizzaImage() {Id = dbItem.Id,  PizzaID = dbItem.PizzaID, MimeType = type, ImageData = data });
            }

            _dataAccess.SaveChanges();

            return Ok();
        }


        #endregion

    }
}