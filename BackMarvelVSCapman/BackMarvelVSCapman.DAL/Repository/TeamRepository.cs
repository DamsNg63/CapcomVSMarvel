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

        public override async Task<Team> Get(int id)
        {
            // TODO: exception if not found
            return await _dbSet.FirstAsync(x => x.TeamId == id);
        }

        public override async Task<bool> Update(Team elem)
        {
            var team = await Get(elem.TeamId);

            if (team == null)
            {
                throw new DbUpdateException("Team with this ID does not exist.");
            }

            team.Name = elem.Name;
          
            return await base.Update(team);
        }
    }
}
