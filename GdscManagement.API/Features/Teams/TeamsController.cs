using AutoMapper;
using GdscManagement.API.Features.Teams.Models;
using GdscManagement.Common.Features.Teams.Models;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.API.Features.Teams
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public TeamsController(IRepository<Team> teamRepository, IRepository<User> userRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TeamResponse>> AddTeam(TeamRequest request)
        {
            var teamLead = await _userRepository.GetAsync(request.TeamLeadId);
            if (teamLead == null)
            {
                return BadRequest($"User with id '{request.TeamLeadId}' does not exist.");
            }

            var team = _mapper.Map<Team>(request);
            team.TeamLead = teamLead;  // 
            var addedTeam = await _teamRepository.AddAsync(team);
            var teamResponse = _mapper.Map<TeamResponse>(addedTeam);

            return Ok(teamResponse);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TeamResponse>>> GetTeams()
        {
            var teams = await _teamRepository.DbSet.Include(q => q.TeamLead).ToListAsync();
            //var teamResponses = _mapper.Map<IEnumerable<TeamResponse>>(teams);

            return Ok(teams.Select(
                teamResponses => _mapper.Map<TeamResponse>(teamResponses)).ToList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<TeamResponse>> GetTeamById(string id)
        {
            var team = await _teamRepository.GetAsync(id);
            
            if (team is null)
            {
                return NotFound("try again xoxo");
            }

            //var teamResponse = _mapper.Map<TeamResponse>(team);

            return Ok(_mapper.Map<TeamResponse>(team));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles=Roles.Admin)]// asa se delimiteaza 
        public async Task<ActionResult<TeamResponse>> DeleteTeam(string id)
        {
            var deletedTeam = await _teamRepository.DeleteAsync(id);
            if (deletedTeam is null)
            {
                return NotFound();
            }

            var teamResponse = _mapper.Map<TeamResponse>(deletedTeam);

            return Ok(teamResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TeamResponse>> UpdateTeam(string id, TeamRequest request)
        {
            var team = await _teamRepository.GetAsync(id);
            if (team is null)
            {
                return NotFound();
            }

            var teamLead = await _userRepository.GetAsync(request.TeamLeadId);
            if (teamLead == null)
            {
                return BadRequest($"User with id '{request.TeamLeadId}' does not exist.");
            }

           

            var updatedTeam = _mapper.Map(request, team);

            var result = await _teamRepository.UpdateAsync(updatedTeam);
            if (result is null)
            {
                return BadRequest();
            }

            var teamResponse = _mapper.Map<TeamResponse>(result);

            return Ok(teamResponse);
        }

        [HttpPut("{id}/teamlead")]
        public async Task<ActionResult<TeamResponse>> ChangeTeamLead(string id, string teamLeadId)
        {
            var team = await _teamRepository.GetAsync(id);
            if (team is null)
            {
                return NotFound();
            }

            var teamLead = await _userRepository.GetAsync(teamLeadId);
            if (teamLead == null)
            {
                return BadRequest($"User with id '{teamLeadId}' does not exist.");
            }

            team.TeamLead = teamLead;

            var result = await _teamRepository.UpdateAsync(team);
            if (result is null)
            {
                return BadRequest();
            }

            var teamResponse = _mapper.Map<TeamResponse>(result);

            return Ok(teamResponse);
        }
    }
}


//repo ul de user profiles, se face un count pt fiecare echipa - members count
