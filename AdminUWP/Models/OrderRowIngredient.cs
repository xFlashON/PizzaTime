using AdminUWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.Models
{
    public class OrderRowIngredient
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public Ingredient Ingredient { get; set; }

    }
}
