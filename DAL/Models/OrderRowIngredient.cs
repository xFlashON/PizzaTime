using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("OrderRowIngredients")]
    public class OrderRowIngredient : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        public float Price { get; set; }

        [Required]
        public OrderRow OrderRow { get; set; }

        [Required]
        public Ingredient Ingredient { get; set; }

    }
}
