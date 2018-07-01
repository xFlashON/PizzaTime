using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PizzaRepository : BaseRepository<Pizza>
    {
        public PizzaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override ICollection<Pizza> GetAll()
        {
            return dbContext.PizzaSet.Include(p => p.Ingredients).ThenInclude(i=>i.Ingredient).ToList();
        }
    }
}
