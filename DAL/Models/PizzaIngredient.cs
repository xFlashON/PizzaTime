using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    [Table("PizzaIngredients")]
    public class PizzaIngredient
    {
        public Guid PizzaId { get; set; }

        public Pizza Pizza {get; set;}

        public Guid IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
