using BackMarvelVSCapman.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.DAL.Repository
{
    public class CharacterRepository : BaseCRUDRepository<Character>
    {
        public CharacterRepository(Context dbContext) : base(dbContext, dbContext.Characters)
        {
        }

        public override async Task<Character> Get(int id)
        {
            // TODO: exception if not found
            return await _dbSet.FirstAsync(x => x.CharacterId == id);
        }
    }
}
