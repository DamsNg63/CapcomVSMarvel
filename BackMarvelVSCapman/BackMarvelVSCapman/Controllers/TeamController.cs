using BackMarvelVSCapman.DAL.Model;
using BackMarvelVSCapman.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackMarvelVSCapman.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        // GET: api/<TeamController>
        [HttpGet]
        public IEnumerable<Team> Get()
        {
            return _teamRepository.GetAll();
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
