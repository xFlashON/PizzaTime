using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaTime.ViewModels
{
    public class ServiceOrderViewModel
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public CustomerViewModel Customer { get; set; }

        public Decimal Total { get; set; }

        public String DeliveryAdress { get; set; }

        public String Comment { get; set; }

        public ICollection<OrderRowViewModel> OrderRows { get; set; }

        public ServiceOrderViewModel()
        {
            OrderRows = new List<OrderRowViewModel>();
        }
    }
}
