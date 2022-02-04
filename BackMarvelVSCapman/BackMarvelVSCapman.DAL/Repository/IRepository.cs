using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public void Add(T team);
        public void Update(T team);
        public void Delete(T team);
    }
}
