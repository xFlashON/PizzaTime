using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("PizzaPrices")]
    public class PizzaPrice:AbstractPrice
    {
        [Required]
        public Pizza Pizza { get; set; } 
    }
}
