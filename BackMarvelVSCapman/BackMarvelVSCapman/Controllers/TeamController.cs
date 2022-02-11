﻿using AutoMapper;
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
    public class TeamController : BaseController
    {
        private readonly IRepository<Team> _teamRepository;

        public TeamController(IRepository<Team> teamRepository, IMapper mapper) : base(mapper)
        {
            _teamRepository = teamRepository;
        }
        // GET: api/<TeamController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TeamDto>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<IEnumerable<Team>, IEnumerable<TeamDto>>(_teamRepository.GetAll()));
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TeamDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                return Created("#", _mapper.Map<Team, TeamDto>(_teamRepository.Get(id)));
            }
            catch (Exception)
            {
                return NotFound(id);
            }
        }

        // POST api/<TeamController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult Post(string value)
        {
            var team = new Team { Name = value, TeamId = 0 };
            return _teamRepository.Add(team) ? Created("#", team) : Conflict();
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult Put([FromBody] Team value)
        {
            return _teamRepository.Update(value) ? Accepted() : Conflict();
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Delete(Team value)
        {
            return _teamRepository.Delete(value) ? Ok() : BadRequest();
        }
    }
}
