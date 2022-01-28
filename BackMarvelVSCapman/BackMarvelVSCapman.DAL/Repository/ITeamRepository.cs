using BackMarvelVSCapman.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Repository
{
    public interface ITeamRepository
    {
        public IEnumerable<Team> GetAll();
        public void Add(Team team);
        public void Update(Team team);
        public void Delete(Team team);
    }
}
