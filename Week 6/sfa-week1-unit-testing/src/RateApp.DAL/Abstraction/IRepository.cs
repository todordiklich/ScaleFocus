using RateApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp.DAL.Abstraction
{
    public interface IRepository<T> where T : Entity
    {
        T Get(Func<T, bool> predicate);

        T Get(int id);

        List<T> All();

        List<T> Find(Func<T, bool> predicate);

        void CreateOrUpdate(T entity);
    }
}
