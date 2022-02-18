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

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public abstract Task<T> Get(int id);

        public async Task<bool> Add(T elem)
        {
            _dbSet.Add(elem);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Update(T elem)
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            _dbSet.Remove(await Get(id));
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
