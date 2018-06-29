// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using PizzaTime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaTime.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Pizza, PizzaViewModel>();

            CreateMap<Ingredient, IngredientViewModel>();

            CreateMap<PizzaIngredient, IngredientViewModel>().ForMember(i=>i.Id, map=>map.MapFrom(i=>i.Ingredient.Id))
            .ForMember(i => i.Name, map => map.MapFrom(i => i.Ingredient.Name)).ReverseMap();

            CreateMap<Customer, CustomerViewModel>().ForMember(c=>c.Password, map => map.Ignore()).ForSourceMember(c=>c.PasswordHash,map=>map.Ignore()).ReverseMap();

            //CreateMap<Order, OrderViewModel>()
            //    .ReverseMap();
        }
    }
}
