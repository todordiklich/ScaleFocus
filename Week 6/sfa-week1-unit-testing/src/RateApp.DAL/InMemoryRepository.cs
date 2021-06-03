using RateApp.DAL.Abstraction;
using RateApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateApp.DAL
{
    public class InMemoryRepository<T> : IRepository<T>  where T : Entity
    {
        private static List<T> Set;

        static InMemoryRepository()
        {
            Set = new List<T>();
        }

        public T Get(Func<T, bool> predicate)
        {
            return Set.FirstOrDefault(predicate);
        }

        public T Get(int id)
        {
           return Set.FirstOrDefault(e => e.Id == id);
        }

        public List<T> All()
        {
            //Create a copy instead of returning the property
            return Set.ToList();
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            return Set.Where(predicate).ToList();
        }

        public void CreateOrUpdate(T entity)
        {
            if (entity.Id != 0)
            {
                T fromMemory = Get(entity.Id);
                Set.Remove(fromMemory);
                Set.Add(entity);
            }
            else
            {
                int id = 0;
                T existing = Set.OrderByDescending(e => e.Id).FirstOrDefault();
                if (existing != null)
                {
                    id = existing.Id + 1;
                }
                else
                {
                    id = 1;
                }
                entity.Id = id;
                Set.Add(entity);
            }
        }
    }
}
