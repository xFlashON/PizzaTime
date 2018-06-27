using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class IngredientPriceRepository : BaseRepository<IngredientPrice>
    {
        public IngredientPriceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
