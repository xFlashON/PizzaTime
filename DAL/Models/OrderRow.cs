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

        public float Price { get; set; }

        public virtual ICollection<OrderRowIngredient> OrderRowIngredients { get; set; }

        public Decimal Amount { get; set; }

        public Decimal Total { get; set; }

        [Required]
        public Order Order { get; set; }

        public OrderRow()
        {
            OrderRowIngredients = new List<OrderRowIngredient>();
        }

    }
}
