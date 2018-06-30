using Microsoft.AspNetCore.Mvc.ModelBinding;
using PizzaTime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaTime.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public DateTime OrderDate { get; set; }

        public CustomerViewModel Customer { get; set; }

        public String DeliveryAdress { get; set; }

        public Decimal Total { get; set; }

        public String Comment { get; set; }

        public ICollection<PizzaViewModel> PizzaList { get; set; }

        public OrderViewModel()
        {
            PizzaList = new List<PizzaViewModel>();
        }
    }
}
