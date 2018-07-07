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

        public override void Update(PizzaImage item)
        {
            var dbItem = dbContext.Set<PizzaImage>().FirstOrDefault(i => i.PizzaID == item.PizzaID);

            if (dbItem != null)
                dbContext.Entry<PizzaImage>(dbItem).CurrentValues.SetValues(item);
        }
    }
}
