using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DAL.Models
{
    [Table("Pizzas")]
    public class Pizza:IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        [Required,ConcurrencyCheck]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<PizzaIngredient> Ingredients { get; set; }

        public Pizza()
        {
            Ingredients = new List<PizzaIngredient>();
            Description = string.Empty;
        }

    }
}