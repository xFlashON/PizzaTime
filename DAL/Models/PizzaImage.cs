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
    [Table("PizzaImages")]
    public class PizzaImage:IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        public Guid PizzaID { get; set; }

        public Pizza Pizza { get; set; }

        public String MimeType { get; set; }

        public Byte[] ImageData { get; set; }
    }
}
