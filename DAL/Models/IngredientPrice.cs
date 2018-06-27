using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("IngredientPrices")]
    public class IngredientPrice:AbstractPrice
    {
        public Ingredient Ingredient { get; set; }
    }
}
