using AdminUWP.Model;
using AdminUWP.Models;
using AdminUWP.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.Infrastructure
{
    class AutomapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Pizza, PizzaViewModel>();
            cfg.CreateMap<Ingredient, IngredientViewModel>();
            cfg.CreateMap<Customer, CustomerViewModel>();
            cfg.CreateMap<Order, OrderViewModel>();
            cfg.CreateMap<OrderRow, OrderRowViewModel>();
            cfg.CreateMap<OrderRowIngredient, OrderRowIngredientsViewModel>();
        }
    }
}
