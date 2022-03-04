using BackMarvelVSCapman.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace BackMarvelVSCapman.DAL
{
    public class Context : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Arena> Arenas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=douard.me;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=isimaISIMA63");
            }
        }
    }
}