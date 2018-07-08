using AutoMapper;
using DAL.Models;
using PizzaTime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaTime.Helpers
{
    public static class ConverterHelper
    {

        public static Order ConvertViewModelToOrder(OrderViewModel vm)
        {
            Order order = new Order
            {
                Id = vm.Id,
                Number = vm.Number,
                DeliveryAdress = vm.DeliveryAdress,
                Date = vm.OrderDate,
                Comment = vm.Comment,
                Total = vm.Total,
                Customer = Mapper.Map<Customer>(vm.Customer)
            };

            foreach (var r in vm.PizzaList)
            {
                OrderRow row = new OrderRow { Order = order, Pizza = Mapper.Map<Pizza>(r), Price = r.Price, Amount = 1, Total = r.Total };

                foreach (var i in r.Ingredients)
                {
                    if (i.Selected)
                    {

                        OrderRowIngredient rowIngredient = new OrderRowIngredient { OrderRow = row, Ingredient = Mapper.Map<Ingredient>(i), Price = i.Price };
                        row.OrderRowIngredients.Add(rowIngredient);

                    }

                }

                order.OrderRows.Add(row);

            }
            return order;
        }


        public static OrderViewModel ConvertOrderToViewModel(Order order)
        {
            OrderViewModel vm = new OrderViewModel()
            {

                Number = order.Number,
                OrderDate = order.Date,
                Total = order.Total,
                DeliveryAdress = order.DeliveryAdress,
                Comment = order.Comment,
                Customer = Mapper.Map<CustomerViewModel>(order.Customer),
                Id = order.Id
            };

            foreach (var r in order.OrderRows)
            {
                PizzaViewModel p = Mapper.Map<PizzaViewModel>(r.Pizza);

                p.Price = r.Price;
                p.Total = r.Total;

                foreach (var i in r.OrderRowIngredients)
                {
                    IngredientViewModel rowIngredient = Mapper.Map<IngredientViewModel>(i.Ingredient);
                    rowIngredient.Price = i.Price;

                    p.Ingredients.Add(rowIngredient);

                }

                vm.PizzaList.Add(p);
            }

            return vm;

        }

    }
}
