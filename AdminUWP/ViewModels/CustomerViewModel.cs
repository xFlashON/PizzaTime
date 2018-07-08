using AdminUWP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUWP.ViewModels
{
    public class CustomerViewModel:ObservableObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public String DeliveryAdress { get; set; }

        public string Role { get; set; }

    }
}
