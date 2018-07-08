using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.Models
{
    public class OrderRow
    {

        public Guid Id { get; set; }

        public Pizza Pizza { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<OrderRowIngredient> OrderRowIngredients { get; set; }

        public Decimal Amount { get; set; }

        public Decimal Total { get; set; }

        public OrderRow()
        {
            OrderRowIngredients = new List<OrderRowIngredient>();
        }

    }
}
