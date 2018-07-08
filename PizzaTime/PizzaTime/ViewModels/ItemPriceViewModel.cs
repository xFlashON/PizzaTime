using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaTime.ViewModels
{
    public class ItemPriceViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}
