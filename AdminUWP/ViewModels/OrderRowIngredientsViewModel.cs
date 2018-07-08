using AdminUWP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.ViewModels
{
    public class OrderRowIngredientsViewModel:ObservableObject
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public IngredientViewModel Ingredient { get; set; }

        public override string ToString()
        {
            return Ingredient?.Name ?? "";
        }
    }
}
