using AutoMapper;
using BackMarvelVSCapman.Business.Gameplay;
using BackMarvelVSCapman.DAL.Model;
using BackMarvelVSCapman.DAL.Repository;
using BackMarvelVSCapman.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public IActionResult CanPlay(Guid playerId, string gameId)
        {
            try
            {
                return Ok(FindById(gameId).CanPlay(playerId));
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }

        [HttpGet("board")]
        [ProducesResponseType(typeof(BoardDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetBoard(string gameId)
        {
            try
            {
                return Ok(
                    new BoardDto
                    {
                        Board = FindById(gameId).RawBoard.Cast<int>().ToArray(),
                        NbCol = Board.NB_COL,
                        NbLin = Board.NB_LIN
                    }
                );
            }
            catch (Exception)
            {
                return NotFound(gameId);
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(GameDto), (int)HttpStatusCode.Created)]
        public IActionResult CreateGame()
        {
            var game = _gameManager.CreateGame(HttpContext.Session.Id);
            return Created("#", new GameDto { GameId = game.GameId, PlayerID = game.Players.P1.PlayerId });
        }

        [HttpPut("join")]
        [ProducesResponseType(typeof(BoardDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult JoinGame(string gameId)
        {
            try
            {
                var game = _gameManager.Games.First(x => x.GameId.ToString() == gameId);
                game.P2Join();
                return Ok(new GameDto { GameId = game.GameId, PlayerID = game.Players.P2.PlayerId });
            }
            catch (Exception)
            {
                return NotFound(gameId);
            }
        }

        [HttpPut("select")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> SelectCharacters(Guid playerId, string gameId, int id1, int id2, int id3)
        {
            try
            {
                return Accepted(_gameManager.Games.First(x => x.GameId.ToString() == gameId).Find(playerId).Characters = (
                        await _characterRepository.Get(id1),
                        await _characterRepository.Get(id2),
                        await _characterRepository.Get(id3)
                    )
                );
            }
            catch (Exception)
            {
                return NotFound(gameId);
            }
        }

        [HttpPut("play")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Play(Guid playerId, string gameId, int col)
        {
            try
            {
                return Ok(FindById(gameId).Play(playerId, col));
            }
            catch (Exception)
            {
                return NotFound(gameId);
            }
        }

        [HttpGet("win")]
        [ProducesResponseType(typeof(WIN_RESULT), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult IsWinGame(string gameId)
        {
            try
            {
                return Ok(FindById(gameId).GetWinResult());
            }
            catch (Exception)
            {
                return NotFound(gameId);
            }
        }

        [HttpGet("ready")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GameReady(string gameId)
        {
            try
            {
                return Ok(FindById(gameId).IsGameReady);
            }
            catch (Exception)
            {
                return NotFound(gameId);
            }
        }

        private Game FindById(string gameId)
        {
            return _gameManager.Games.First(x => x.GameId.ToString() == gameId);
        }
    }
}
