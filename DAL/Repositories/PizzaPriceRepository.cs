using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PizzaPriceRepository : BaseRepository<PizzaPrice>
    {
        public PizzaPriceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
