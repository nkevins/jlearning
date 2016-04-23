using DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbContext context;
        public DbSet<T> dbset;
        public Repository(DbContext context)
        {
            this.context = context;
            dbset = context.Set<T>();
        }
        public Repository()
        {
            this.context = new Context();
            dbset = context.Set<T>();
        }


        public T GetById(int id)
        {
            return dbset.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return dbset;
        }
        public void Insert(T entity)
        {
            dbset.Add(entity);
        }

        public void Edit(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
       
    }
}
