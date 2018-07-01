using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Customers")]
    public class Customer : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        [Required, MaxLength(100), ConcurrencyCheck]
        public String Name { get; set; }

        [Required, MaxLength(100)]
        public String Email { get; set; }

        [Required,MaxLength(25)]
        public String PhoneNumber { get; set; }

        [Required]
        public String DeliveryAdress { get; set; }

        [Required]
        public String Role {get; set;}

        [Required]
        public String PasswordHash { get; set; }

        public Customer()
        {
            DeliveryAdress = "";
        }

    }
}
