using AutoMapper;
using BackMarvelVSCapman.DAL.Model;
using BackMarvelVSCapman.DAL.Repository;
using BackMarvelVSCapman.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackMarvelVSCapman.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : BaseController
    {
        private readonly IRepository<Team> _teamRepository;

        public TeamController(IRepository<Team> teamRepository, IMapper mapper) : base(mapper)
        {
            _teamRepository = teamRepository;
        }
        // GET: api/<TeamController>
        [HttpGet]
        public IEnumerable<TeamDto> Get()
        {
            return _mapper.Map<IEnumerable<Team>, IEnumerable<TeamDto>>(_teamRepository.GetAll());
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public TeamDto Get(int id)
        {
            return _mapper.Map<Team, TeamDto>(_teamRepository.Get(id));
        }

        // POST api/<TeamController>
        [HttpPost]
        public void Post(string value)
        {
            _teamRepository.Add(new Team { Name = value, TeamId = 0});
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Team value)
        {
            _teamRepository.Update(value);
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public void Delete(Team value)
        {
            _teamRepository.Delete(value);
        }
    }
}
