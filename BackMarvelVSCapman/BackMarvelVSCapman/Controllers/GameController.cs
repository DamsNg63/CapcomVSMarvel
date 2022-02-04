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

        [HttpPost]
        public GameDto CreateGame()
        {
            return _mapper.Map<Game, GameDto>(_gameManager.CreateGame(HttpContext.Session.Id));
        }

        [HttpPut("join")]
        public void JoinGame(string gameId)
        {
            _gameManager.Games.First(x => x.GameId.ToString() == gameId).Players.P2.SessionId = HttpContext.Session.Id;
        }

        [HttpPut("select")]
        public void SelectCharacters(string gameId, int id1, int id2, int id3)
        {
            _gameManager.Games.First(x => x.GameId.ToString() == gameId).Find(HttpContext.Session.Id).Characters = (
                _characterRepository.Get(id1),
                _characterRepository.Get(id2),
                _characterRepository.Get(id3)
            );
        }

        private Game FindById(string gameId)
        {
            // TODO: exception if game not found
            return _gameManager.Games.First(x => x.GameId.ToString() == gameId);
        }
    }
}
