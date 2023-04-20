using AutoMapper;
using GdscManagement.API.Features.Teams.Models;
using GdscManagement.Common.Features.Teams.Models;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<TeamResponse>> AddTeam(TeamRequest request)
        {
            var teamLead = await _userRepository.GetAsync(request.TeamLeadId).ConfigureAwait(false);
            if (teamLead == null)
            {
                return BadRequest($"User with id '{request.TeamLeadId}' does not exist.");
            }

            var team = _mapper.Map<Team>(request);
            team.TeamLead = teamLead;

            var addedTeam = await _teamRepository.AddAsync(team).ConfigureAwait(false);
            var teamResponse = _mapper.Map<TeamResponse>(addedTeam);

            return Ok(teamResponse);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamResponse>>> GetTeams()
        {
            var teams = await _teamRepository.GetAsync();
            var teamResponses = _mapper.Map<IEnumerable<TeamResponse>>(teams);

            return Ok(teamResponses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamResponse>> GetTeamById(string id)
        {
            var team = await _teamRepository.GetAsync(id);
            if (team is null)
            {
                return NotFound();
            }

            var teamResponse = _mapper.Map<TeamResponse>(team);

            return Ok(teamResponse);
        }

        [HttpDelete("{id}")]
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

            var teamLead = await _userRepository.GetAsync(request.TeamLeadId).ConfigureAwait(false);
            if (teamLead == null)
            {
                return BadRequest($"User with id '{request.TeamLeadId}' does not exist.");
            }

            team.TeamLead = teamLead;

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

            var teamLead = await _userRepository.GetAsync(teamLeadId).ConfigureAwait(false);
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