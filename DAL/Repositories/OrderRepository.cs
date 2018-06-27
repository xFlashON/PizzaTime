using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Order GetById(Guid Id)
        {
            return dbContext.OrderSet.Include(o=>o.OrderRows)
                .ThenInclude(or => or.OrderRowIngredients).FirstOrDefault(o=>o.Id==Id) ;
        }

        public override IEnumerable<Order> GetAll()
        {
            return dbContext.OrderSet.Include(o => o.OrderRows).
                ThenInclude(or => or.OrderRowIngredients).ToList();
        }

        public override void Update(Order item)
        {
            base.Update(item);
        }
    }
}
