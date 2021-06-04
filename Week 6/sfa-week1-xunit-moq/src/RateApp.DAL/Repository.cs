using RateApp.DAL.Abstraction;
using RateApp.DAL.Data;
using RateApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp.DAL
{
    public class Repository<T> : IRepository<T> where T : Entity
    {

        private readonly DatabaseContext _databaseContext;

        public Repository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public List<T> All()
        {
            return _databaseContext.Set<T>().ToList();
        }

        public void CreateOrUpdate(T entity)
        {
            if (entity.Id != 0)
            {
                entity.LastUpdate = DateTime.Now;
                _databaseContext.Set<T>().Update(entity);
            }
            else
            {
                _databaseContext.Add(entity);
            }
            _databaseContext.SaveChanges();
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            return _databaseContext.Set<T>().Where(predicate).ToList();
        }

        public T Get(Func<T, bool> predicate)
        {
            return _databaseContext.Set<T>().FirstOrDefault(predicate);
        }

        public T Get(int id)
        {
            return _databaseContext.Set<T>().FirstOrDefault(e => e.Id == id);
        }
    }
}
