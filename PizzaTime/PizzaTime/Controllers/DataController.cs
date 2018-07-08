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
        [ResponseCache(Duration = 60)]
        public IActionResult GetImage(string id)
        {
            if (id is null)
                return new NotFoundResult();

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
            {
                var item = Mapper.Map<Pizza>(pizza);

                _dataAccess.Pizzas.Create(item);

                pizza = Mapper.Map<PizzaViewModel>(item);
            }

            else
                _dataAccess.Pizzas.Update(Mapper.Map<Pizza>(pizza));

            _dataAccess.SaveChanges();

            return Ok(pizza);

        }


        [HttpPost, Authorize(Roles = ("Admin"))]
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

        [HttpPost, Authorize(Roles = ("Admin"))]
        public IActionResult SaveIngredient([FromBody] PizzaViewModel ingredient)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (ingredient.Id == Guid.Empty)
                _dataAccess.Ingredients.Create(Mapper.Map<Ingredient>(ingredient));
            else
                _dataAccess.Ingredients.Update(Mapper.Map<Ingredient>(ingredient));

            _dataAccess.SaveChanges();

            return Ok(ingredient);

        }

        [HttpPost, Authorize(Roles = ("Admin"))]
        public IActionResult DeletePizza (string id)
        {

            if (id is null)
                return new BadRequestResult();

            if (_dataAccess.Orders.Get(o => o.OrderRows.Any(or => or.Pizza.Id.ToString() == id)).Any())
                return new StatusCodeResult(StatusCodes.Status304NotModified);

            var dbItem = _dataAccess.Pizzas.GetById(new Guid(id));

            if (dbItem is null)
                return new NotFoundResult();


            _dataAccess.Pizzas.Delete(dbItem);

            _dataAccess.SaveChanges();

            return Ok(true);
        }

        [HttpPost, Authorize(Roles = ("Admin"))]
        public IActionResult DeleteIngredient(string id)
        {

            if (id is null)
                return new BadRequestResult();

            if (_dataAccess.Orders.Get(o => o.OrderRows.Any(or => or.OrderRowIngredients.Any(i=>i.Ingredient.Id.ToString() == id))).Any()
                || _dataAccess.Pizzas.Get(p=>p.Ingredients.Any(i=>i.IngredientId.ToString()==id)).Any())
                return new StatusCodeResult(StatusCodes.Status304NotModified);

            var dbItem = _dataAccess.Ingredients.GetById(new Guid(id));

            if (dbItem is null)
                return new NotFoundResult();


            _dataAccess.Ingredients.Delete(dbItem);

            _dataAccess.SaveChanges();

            return Ok();
        }

        #endregion

    }
}