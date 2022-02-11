using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> GetAll();
        public Task<T> Get(int id);
        public Task<bool> Add(T team);
        public Task<bool> Update(T team);
        public Task<bool> Delete(int id);
    }
}
