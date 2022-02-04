using BackMarvelVSCapman.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Repository
{
    public class TeamRepository : BaseCRUDRepository<Team>
    {
        public TeamRepository(Context context) : base(context, context.Teams)
        {
        }

        public override Team Get(int id)
        {
            // TODO: exception if not found
            return _dbSet.First(x => x.TeamId == id);
        }
    }
}
