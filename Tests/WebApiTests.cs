using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PizzaTime.Controllers;
using PizzaTime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Tests
{
    [TestClass]
    public class WebApiTests
    {
        [TestMethod]
        public void GetMenu()
        {

            var pizzaList = new List<Pizza>();
            var pizzaPriceList = new List<PizzaPrice>();
            var ingredientPriceList = new List<IngredientPrice>();

            Pizza pizza1 = new Pizza { Name = "Pizza1" };
            Pizza pizza2 = new Pizza { Name = "Pizza2" };

            Ingredient ingredient1 = new Ingredient { Name = "Ingredient 1" };
            Ingredient ingredient2 = new Ingredient { Name = "Ingredient 2" };

            pizza1.Ingredients.Add(new PizzaIngredient { Pizza = pizza1, Ingredient = ingredient1, IngredientId = ingredient1.Id });
            pizza1.Ingredients.Add(new PizzaIngredient { Pizza = pizza1, Ingredient = ingredient2, IngredientId = ingredient2.Id });

            pizzaList.Add(pizza1);
            pizzaList.Add(pizza2);

            IngredientPrice ingredientPrice = new IngredientPrice { Date = new DateTime(2018, 01, 01), Ingredient = ingredient1, Price = 10 };
            IngredientPrice ingredientPrice2 = new IngredientPrice { Date = new DateTime(2019, 01, 01), Ingredient = ingredient1, Price = 15 };

            PizzaPrice pizzaPrice = new PizzaPrice { Date = new DateTime(2018, 01, 01), Pizza = pizza1, Price = 100 };
            PizzaPrice pizzaPrice2 = new PizzaPrice { Date = new DateTime(2018, 01, 01), Pizza = pizza2, Price = 110 };

            pizzaPriceList.Add(pizzaPrice);
            pizzaPriceList.Add(pizzaPrice2);

            ingredientPriceList.Add(ingredientPrice);
            ingredientPriceList.Add(ingredientPrice2);

            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());

            var mock = new Mock<IDataAccess>();
            mock.Setup(r => r.Pizzas.GetAll()).Returns(pizzaList);

            mock.Setup(m => m.PizzaPrices.Get(It.IsAny<Expression<Func<PizzaPrice, bool>>>())).Returns<Expression<Func<PizzaPrice, bool>>>((func) =>
            {
                return pizzaPriceList.Where(func.Compile()).ToList();
            });

            mock.Setup(m => m.IngredientPrices.Get(It.IsAny<Expression<Func<IngredientPrice, bool>>>())).Returns<Expression<Func<IngredientPrice, bool>>>((func) =>
            {
                return ingredientPriceList.Where(func.Compile()).ToList();
            });

            DataController controller = new DataController(mock.Object, null);

            var result = controller.GetMenu();

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            Assert.IsNotNull(((OkObjectResult)result).Value);

            Assert.AreEqual(((ICollection<PizzaViewModel>)((OkObjectResult)result).Value).Count, 2);

        }

        [TestMethod]
        public void GetImage()
        {
            var imagesList = new List<PizzaImage>();

            var mock = new Mock<IDataAccess>();
            mock.Setup(r => r.PizzaImages.GetById(It.IsAny<Guid>())).Returns<Guid>(id=>imagesList.Where(i=>i.Id == id).FirstOrDefault());

            DataController controller = new DataController(mock.Object, null);

            var result = controller.GetImage(null);
            var result2 = controller.GetImage((new Guid()).ToString());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsInstanceOfType(result2, typeof(VirtualFileResult));

        }
    }
}
