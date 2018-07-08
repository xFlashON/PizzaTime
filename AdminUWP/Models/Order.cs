using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.Models
{
    public class Order
    {

        public Guid Id { get; set; }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public Customer Customer { get; set; }

        public Decimal Total { get; set; }

        public String DeliveryAdress { get; set; }

        public String Comment { get; set; }

        public ICollection<OrderRow> OrderRows { get; set; }

        public Order()
        {
            OrderRows = new List<OrderRow>();
        }
    }
}
