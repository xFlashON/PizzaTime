using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PizzaImageRepository:BaseRepository<PizzaImage>
    {
        public PizzaImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
