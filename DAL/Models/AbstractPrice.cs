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
    public abstract class AbstractPrice:IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        [Required,ConcurrencyCheck]
        public DateTime Date { get; set; }

        [Required, ConcurrencyCheck]
        public decimal Price { get; set; }
    }
}
