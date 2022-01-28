using BackMarvelVSCapman.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Repository
{
    public interface ICharacterRepository
    {
        public IEnumerable<Character> GetAll();
        public void Add(Character character);
        public void Update(Character character);
        public void Delete(Character character);
    }
}
