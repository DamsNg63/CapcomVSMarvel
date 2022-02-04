using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.Business.Gameplay
{
    public class GameManager : IGameManager
    {
        public IReadOnlyCollection<Game> Games => _games.AsReadOnly();
        private readonly List<Game> _games;

        public GameManager()
        {
            _games = new List<Game>();
        }

        public Game CreateGame(string sessionId)
        {
            Player p1 = new();
            p1.SessionId = sessionId;
            
            Game game = new(p1, new Player());
            Add(game);

            return game;
        }

        public void Add(Game game)
        {
            _games.Add(game);
        }

        public void Remove(Game game)
        {
            _games.Remove(game);
        }

    }
}
