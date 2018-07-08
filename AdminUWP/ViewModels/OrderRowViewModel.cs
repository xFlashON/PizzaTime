using AdminUWP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.ViewModels
{
    public class OrderRowViewModel:ObservableObject
    {
        public Guid Id { get; set; }

        public PizzaViewModel Pizza { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<OrderRowIngredientsViewModel> OrderRowIngredients { get; set; }

        public Decimal Amount { get; set; }

        public Decimal Total { get; set; }

        public string IngredientsListDisplay
        {
            get
            {
                return string.Join(Environment.NewLine, OrderRowIngredients);
            }
        }

        public OrderRowViewModel()
        {
            OrderRowIngredients = new List<OrderRowIngredientsViewModel>();
        }

    }
}
