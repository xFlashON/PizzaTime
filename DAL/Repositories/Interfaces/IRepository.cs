using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository <T> where T:IEntity
    {
        T GetById(Guid Id);
        IEnumerable<T> Get(Expression<Func<T,bool>> func);
        IEnumerable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(T item);

    }
}
