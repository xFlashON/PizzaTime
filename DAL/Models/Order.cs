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

    [Table("Orders")]
    public class Order:IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Customer Customer { get; set; }

        public Decimal Total { get; set; }

        public String Comment { get; set; }

        public ICollection<OrderRow> OrderRows { get; set; }

        public Order()
        {
            OrderRows = new List<OrderRow>();
        }

    }
}
