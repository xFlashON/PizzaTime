﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaTime.ViewModels
{
    public class PizzaViewModel
    {

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Total { get; set; }

        public ICollection<IngredientViewModel> Ingredients { get; set; }

        public PizzaViewModel()
        {
            Ingredients = new List<IngredientViewModel>();
        }

    }
}
