using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected ApplicationDbContext dbContext;

        public BaseRepository(ApplicationDbContext container) {
            this.dbContext = container;
        }

        public virtual T GetById(Guid Id)
        {
            return dbContext.Set<T>().FirstOrDefault(i => i.Id == Id);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> func)
        {
            return dbContext.Set<T>().Where(func);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>();
        }

        public virtual void Create(T item)
        {
            dbContext.Entry<T>(item).State = EntityState.Added;
        }

        public virtual void Delete(T item)
        {
            var dbItem = dbContext.Set<T>().FirstOrDefault(i => i.Id == item.Id);

            if (dbItem != null)
                dbContext.Entry<T>(dbItem).State = EntityState.Deleted;
        }

        public virtual void Update(T item)
        {
            var dbItem = dbContext.Set<T>().FirstOrDefault(i => i.Id == item.Id);

            if (dbItem != null)
                dbContext.Entry<T>(dbItem).CurrentValues.SetValues(item);
        }
    }
}
