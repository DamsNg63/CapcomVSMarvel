using AutoMapper;
using BackMarvelVSCapman.DAL.Model;
using BackMarvelVSCapman.DAL.Repository;
using BackMarvelVSCapman.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackMarvelVSCapman.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArenaController : BaseController
    {
        private readonly IRepository<Arena> _arenaRepository;

        public ArenaController( IRepository<Arena> arenaRepository, IMapper mapper) : base(mapper) { 
            _arenaRepository = arenaRepository;
        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IEnumerable<ArenaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = _mapper.Map<IEnumerable<Arena>, IEnumerable<ArenaDto>>(await _arenaRepository.GetAll());
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Post(CreateArenaDto arenaToCreate)
        {
            var result = await _arenaRepository.Add(_mapper.Map<CreateArenaDto, Arena>(arenaToCreate));
            return result ? Created("#", arenaToCreate) : Conflict(arenaToCreate);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArenaDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int arenaId)
        {
            try
            {
                return Ok(_mapper.Map<Arena, ArenaDto>(await _arenaRepository.Get(arenaId)));
            }
            catch (Exception)
            {
                return NotFound(arenaId);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Put([FromBody] ArenaDto value)
        {
            return await _arenaRepository.Update(_mapper.Map<ArenaDto, Arena>(value)) ? Accepted() : Conflict(value);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            return await _arenaRepository.Delete(id) ? Ok() : BadRequest();
        }
    }


}
