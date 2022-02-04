using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.Business.Gameplay
{
    public class Game : IEquatable<Game>
    {
        public Guid GameId { get; }
        public (Player P1, Player P2) Players { get; set; }

        public bool IsGameReady
            => Players.P1.SessionId != string.Empty && Players.P2.SessionId != string.Empty // Both players have a session
               && Players.P1.Characters.HasValue && Players.P2.Characters.HasValue;         // Characters have been selected

        public Game(Player p1, Player p2)
        {
            GameId = Guid.NewGuid();
            Players = (p1, p2);
        }

        public Player Find(string sessionId)
        {
            return Players.P1.SessionId == sessionId ? Players.P1 : Players.P2;
        }

        public bool Equals(Game? other)
        {
            return GameId.Equals(other?.GameId);
        }
    }
}
