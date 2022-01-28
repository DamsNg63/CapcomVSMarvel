using BackMarvelVSCapman.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Repository
{
    public class TeamRepository : BaseCRUDRepository<Team>, ITeamRepository
    {
        public TeamRepository(Context context) : base(context, context.Teams)
        {
        }
    }
}
