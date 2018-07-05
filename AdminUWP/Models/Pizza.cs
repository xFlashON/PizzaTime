using AdminUWP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.Models
{
    public class Pizza
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }

        public Pizza()
        {
            Ingredients = new List<Ingredient>();
        }

    }
}
