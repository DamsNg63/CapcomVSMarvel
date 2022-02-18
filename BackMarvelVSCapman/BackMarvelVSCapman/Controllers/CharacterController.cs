using AutoMapper;
using BackMarvelVSCapman.Business.Services;
using BackMarvelVSCapman.DAL.Model;
using BackMarvelVSCapman.DAL.Repository;
using BackMarvelVSCapman.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        [ProducesResponseType(typeof(IEnumerable<CharacterDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Get()
        {
            var result = _mapper.Map<IEnumerable<Character>, IEnumerable<CharacterDto>>(await _characterRepository.GetAll());
            return result.Any() ? Ok(result) : NoContent();
        }

        // GET api/<CharacterController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CharacterDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(_mapper.Map<Character, CharacterDto>(await _characterRepository.Get(id)));
            }
            catch (Exception)
            {
                return NotFound(id);
            }
        }

        // POST api/<CharacterController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Post(CreateChraraterDto value)
        {   
            return await _characterService.Create(value) ? Created("#", value) : Conflict(value);
        }

        // PUT api/<CharacterController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Put([FromBody] CharacterDto value)
        {
            return await _characterRepository.Update(_mapper.Map<CharacterDto, Character>(value)) ? Accepted() : Conflict(value);
        }

        // DELETE api/<CharacterController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            return await _characterRepository.Delete(id) ? Ok() : BadRequest(id);
        }
    }
}
