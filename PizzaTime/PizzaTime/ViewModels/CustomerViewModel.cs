using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaTime.ViewModels
{
    public class CustomerViewModel
    {

        public Guid Id { get; set; }

        [Required, MinLength(3)]
        public String Name { get; set; }

        [Required]
        public String Password { get; set; }

        [Required]
        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public String DeliveryAdress { get; set; }

    }
}
