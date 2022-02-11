using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Repository
{
    public abstract class BaseCRUDRepository<T> : IRepository<T> where T : class
    {
        protected Context _dbContext;
        protected DbSet<T> _dbSet;
        public BaseCRUDRepository(Context dbContext, DbSet<T> dbSet)
        {
            _dbContext = dbContext;
            _dbSet = dbSet;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public abstract T Get(int id);

        public bool Add(T elem)
        {
            _dbSet.Add(elem);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(T elem)
        {
            _dbSet.Update(elem);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(T elem)
        {
            _dbSet.Remove(elem);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
