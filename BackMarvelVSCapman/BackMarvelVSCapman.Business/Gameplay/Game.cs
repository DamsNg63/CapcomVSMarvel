using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMarvelVSCapman.Business.Gameplay
{
    public class Game
    {
        public Guid GameId { get; }
        public (Player P1, Player P2) Players { get; set; }
        private Guid _nextPlayerToPlay { get; set; }

        private Board _board;
        public Token[,] RawBoard => _board.TabBoard;

        private bool _isP2Join = false;

        public bool IsGameReady
            => _isP2Join; // Both players have join
    //           && Players.P1.Characters.HasValue && Players.P2.Characters.HasValue;         // Characters have been selected

        public Game(Player p1, Player p2)
        {
            GameId = Guid.NewGuid();
            Players = (p1, p2);
            _board = new Board();
            _nextPlayerToPlay = p1.PlayerId;
        }

        public void P2Join()
        {
            _isP2Join = true;
        }

        public Player Find(Guid playerId)
        {
            bool isP1 = Players.P1.PlayerId == playerId;
            bool isP2 = Players.P2.PlayerId == playerId;

            if (!isP1 && !isP2)
            {
                throw new ArgumentException("Wrong player ID.");
            }

            return isP1 ? Players.P1 : Players.P2;
        }

        public bool CanPlay(Guid playerId)
        {
            return _nextPlayerToPlay == playerId;
        }

        public bool Play(Guid playerId, int col)
        {
            var player = Find(playerId);

            if (_nextPlayerToPlay == player.PlayerId && IsGameReady)
            {
                var played = _board.Play(col, player.PlayerId == Players.P1.PlayerId);
                if (played)
                {
                    _nextPlayerToPlay = _nextPlayerToPlay == Players.P1.PlayerId ? Players.P2.PlayerId : Players.P1.PlayerId;
                }
                return played;
            }

            return false;
        }

        public WIN_RESULT GetWinResult()
        {
            return _board.CheckWin();
        }

        public bool Equals(Game? other)
        {
            return GameId.Equals(other?.GameId);
        }
    }
}
