using AutoMapper;
using BackMarvelVSCapman.Business.Services;
using BackMarvelVSCapman.DAL.Model;
using BackMarvelVSCapman.DAL.Repository;
using BackMarvelVSCapman.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackMarvelVSCapman.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : BaseController
    {
        private readonly ICharacterService _characterService;
        private readonly IRepository<Character> _characterRepository;
        public CharacterController(IRepository<Character> characterRepository, ICharacterService characterService, IMapper mapper) : base(mapper)
        {
            _characterService = characterService;
            _characterRepository = characterRepository;
        }
        // GET: api/<CharacterController>
        [HttpGet]
        public IEnumerable<CharacterDto> Get()
        {
            return _mapper.Map<IEnumerable<Character>, IEnumerable<CharacterDto>>(_characterRepository.GetAll());
        }

        // GET api/<CharacterController>/5
        [HttpGet("{id}")]
        public CharacterDto Get(int id)
        {
            return _mapper.Map<Character, CharacterDto>(_characterRepository.Get(id));
        }

        // POST api/<CharacterController>
        [HttpPost]
        public void Post(CreateChraraterDto value)
        {
            _characterService.Create(value);
        }

        // PUT api/<CharacterController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Character value)
        {
            _characterRepository.Update(value);
        }

        // DELETE api/<CharacterController>/5
        [HttpDelete("{id}")]
        public void Delete([FromBody] Character value)
        {
            _characterRepository.Delete(value);
        }
    }
}
