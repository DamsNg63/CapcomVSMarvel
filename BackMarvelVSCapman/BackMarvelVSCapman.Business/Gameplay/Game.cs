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
            return Players.P1.PlayerId == playerId ? Players.P1 : Players.P2;
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
                    if (_nextPlayerToPlay == Players.P1.PlayerId)
                    {
                        _nextPlayerToPlay = Players.P2.PlayerId;
                    }
                    else
                    {
                        _nextPlayerToPlay = Players.P1.PlayerId;
                    }
                }
                return played;
            }
            else
            {
                return false;
            }
        }

        public WIN_RESULT GetWinResult()
        {
            return _board.CheckWin();
        }

        public bool Equals(Game? other)
        {
            return GameId.Equals(other?.GameId);
        }

        public Jeton[] getBoard()
        {
            return _board.TabBoard;
        }
    }
}
