using AutoMapper;
using BackMarvelVSCapman.Business.Gameplay;
using BackMarvelVSCapman.DAL.Model;
using BackMarvelVSCapman.DAL.Repository;
using BackMarvelVSCapman.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackMarvelVSCapman.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly IGameManager _gameManager;
        private readonly IRepository<Character> _characterRepository;

        public GameController(IGameManager gameManager, IRepository<Character> characterRepository, IMapper mapper) : base(mapper)
        {
            _gameManager = gameManager;
            _characterRepository = characterRepository;
        }

        [HttpGet("canplay")]
        public bool CanPlay(Guid playerId, string gameId)
        {
            var game = FindById(gameId);
            return game.CanPlay(playerId);
        }

        [HttpGet("board")]
        public BoardDto getBorad(string gameId)
        {
            var game = FindById(gameId);
            return new BoardDto { Board = game.getBoard().Cast<int>().ToArray(), NbCol = Board.NB_COL, NbLin = Board.NB_LIN };
        }

        [HttpPost("create")]
        public GameDto CreateGame()
        {
            var game = _gameManager.CreateGame(HttpContext.Session.Id);
            return new GameDto { GameId = game.GameId, PlayerID = game.Players.P1.PlayerId };
        }

        [HttpPut("join")]
        public GameDto JoinGame(string gameId)
        {
            var game = _gameManager.Games.First(x => x.GameId.ToString() == gameId);
            game.P2Join();
            return new GameDto { GameId = game.GameId, PlayerID = game.Players.P2.PlayerId };
        }

        [HttpPut("select")]
        public void SelectCharacters(Guid playerId, string gameId, int id1, int id2, int id3)
        {
            _gameManager.Games.First(x => x.GameId.ToString() == gameId).Find(playerId).Characters = (
                _characterRepository.Get(id1),
                _characterRepository.Get(id2),
                _characterRepository.Get(id3)
            );
        }

        [HttpPut("play")]
        public bool Play(Guid playerId, string gameId, int col)
        {
            return FindById(gameId).Play(playerId, col);
        }

        [HttpGet("win")]
        public WIN_RESULT IsWinGame(string gameId)
        {
            return FindById(gameId).GetWinResult();
        }

        [HttpGet("ready")]
        public bool GameReady(string gameId)
        {
            return FindById(gameId).IsGameReady;
        }

        private Game FindById(string gameId)
        {
            // TODO: exception if game not found
            return _gameManager.Games.First(x => x.GameId.ToString() == gameId);
        }
    }
}
