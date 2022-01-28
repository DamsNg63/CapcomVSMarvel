using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Repository
{
    public abstract class BaseCRUDRepository<T> where T : class
    {
        private Context _dbContext;
        private DbSet<T> _dbSet;
        public BaseCRUDRepository(Context dbContext, DbSet<T> dbSet)
        {
            _dbContext = dbContext;
            _dbSet = dbSet;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public void Add(T elem)
        {
            _dbSet.Add(elem);
            _dbContext.SaveChanges();
        }

        public void Update(T elem)
        {
            _dbSet.Update(elem);
            _dbContext.SaveChanges();
        }

        public void Delete(T elem)
        {
            _dbSet.Remove(elem);
            _dbContext.SaveChanges();
        }
    }
}
