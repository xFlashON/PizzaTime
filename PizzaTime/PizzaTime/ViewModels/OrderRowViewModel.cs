using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaTime.ViewModels
{
    public class OrderRowViewModel
    {
        public Guid Id { get; set; }

        public PizzaViewModel Pizza { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<OrderRowIngredientViewModel> OrderRowIngredients { get; set; }

        public Decimal Amount { get; set; }

        public Decimal Total { get; set; }

        public OrderRowViewModel()
        {
            OrderRowIngredients = new List<OrderRowIngredientViewModel>();
        }
    }
}
