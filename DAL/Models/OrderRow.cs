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
    [Table("OrderRows")]
    public class OrderRow : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        [Required]
        public Pizza Pizza { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<OrderRowIngredient> OrderRowIngredients { get; set; }

        [Required]
        public Decimal Amount { get; set; }

        [Required]
        public Decimal Total { get; set; }

        [Required]
        public Order Order { get; set; }

        public OrderRow()
        {
            OrderRowIngredients = new List<OrderRowIngredient>();
        }

    }
}
