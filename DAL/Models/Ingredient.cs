using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DAL.Models
{
    [Table("Ingredients")]
    public class Ingredient : IEntity

    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid Id { get; set; }

        [Required,ConcurrencyCheck]
        public string Name { get; set; }

    }
}