using BackMarvelVSCapman.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Repository
{
    public class ArenaRepository : BaseCRUDRepository<Arena>
    {
        public ArenaRepository(Context dbContext) : base(dbContext, dbContext.Arenas)
        {
        }

        public override async Task<Arena> Get(int id)
        {
            return await _dbSet.FirstAsync(x => x.ArenaId == id);
        }

        public override async Task<bool> Update(Arena elem)
        {
            var arena = await Get(elem.ArenaId);

            if (arena == null)
            {
                throw new DbUpdateException("Arena with this ID does not exist.");
            }

            elem.ArenaId = arena.ArenaId;
            elem.Name = arena.Name;
            elem.Image = arena.Image;

            return await base.Update(elem);
        }
    }
}
