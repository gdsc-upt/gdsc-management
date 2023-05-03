using AutoMapper;
using GdscManagement.API.Features.Teams.Models;
using GdscManagement.Common.Features.Teams.Models;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Features.UsersProfile.Models;
using GdscManagement.Common.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.API.Features.UsersProfile;

[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly IRepository<UserProfile> _userProfileRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Team> _teamRepository;
    private readonly IMapper _mapper;

    public UserProfileController(IRepository<UserProfile> userProfileRepository, IRepository<User> userRepository, IRepository<Team> teamRepository, IMapper mapper)
    {
        _userProfileRepository = userProfileRepository;
        _userRepository = userRepository;
        _teamRepository = teamRepository;
        _mapper = mapper;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserProfileResponse>> AddUserProfile(UserProfileRequest request)
    {
        var user = await _userRepository.GetAsync(request.UserId);
        if (user == null)
        {
            return NotFound($"User with id '{request.UserId}' does not exist.");
        }
        
        var team = await _teamRepository.GetAsync(request.TeamId);
        if (team == null)
        {
            return NotFound($"User with id '{request.TeamId}' does not exist.");
        }

        var userProfile = _mapper.Map<UserProfile>(request);
        userProfile.User = user;
        userProfile.Team = team;

        var addedUserProfile = await _userProfileRepository.AddAsync(userProfile);
        var userProfileResponse = _mapper.Map<UserProfileResponse>(addedUserProfile);

        return Ok(userProfileResponse);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserProfileResponse>>> GetUserProfiles()
    {
        var userProfiles = await _userProfileRepository.DbSet
            .Include(up => up.User)
            .Include(up => up.Team)
            .ToListAsync();
        // var userProfileResponses = _mapper.Map<IEnumerable<UserProfileResponse>>(userProfiles);

        return Ok(userProfiles.Select(
            userProfilesResponses => _mapper.Map<UserProfileResponse>(userProfilesResponses)).ToList());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserProfileResponse>> GetUserProfileById(string id)
    {
        var userProfile = await _userProfileRepository.DbSet
            .Include(up => up.User)
            .Include(up => up.Team)
            .FirstOrDefaultAsync(up => up.Id == id);
        if (userProfile is null)
        {
            return NotFound("User Profile Not Found");
        }

        // var userProfileResponse = _mapper.Map<UserProfileResponse>(userProfile);

        return Ok(_mapper.Map<UserProfileResponse>(userProfile));
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles=Roles.Admin)]
    public async Task<ActionResult<UserProfileResponse>> DeleteUserProfile(string id)
    {
        var deletedUserProfile = await _userProfileRepository.DbSet
            .Include(up => up.User)
            .Include(up => up.Team)
            .FirstOrDefaultAsync(up => up.Id == id);
        if (deletedUserProfile is null)
        {
            return NotFound("User Profile Not Found");
        }
        await _userProfileRepository.DeleteAsync(id);

        var userProfileResponse = _mapper.Map<UserProfileResponse>(deletedUserProfile);

        return Ok(userProfileResponse);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<UserProfileResponse>> UpdateUserProfile(string id, UserProfileRequest request)
    {
        var userProfile = await _userProfileRepository.DbSet
            .Include(up => up.User)
            .Include(up => up.Team)
            .FirstOrDefaultAsync(up => up.Id == id);
        if (userProfile is null)
        {
            return NotFound($"UserProfile with id '{id}' does not exist.");
        }

        var user = await _userRepository.GetAsync(request.UserId);
        if (user == null)
        {
            return NotFound($"User with id '{request.UserId}' does not exist.");
        }

        var team = await _teamRepository.GetAsync(request.TeamId);
        if (team == null)
        {
            return NotFound($"Team with id '{request.TeamId}' does not exist.");
        }
        
        // TODO: Daca pun user sau Team diferit nu merge ???????
        var updatedUserProfile = _mapper.Map(request, userProfile);
        updatedUserProfile.User = user;
        updatedUserProfile.Team = team;

        var result = await _userProfileRepository.UpdateAsync(updatedUserProfile);
        if (result is null)
        {
            return NotFound("Error updating user profile in db");
        }

        var userProfileResponse = _mapper.Map<UserProfileResponse>(result);

        return Ok(userProfileResponse);
    }
    
    [HttpPatch("{id}/{teamId}")]
    public async Task<ActionResult<UserProfileResponse>> UpdateUserProfileTeam(string id, string teamId)
    {
        var userProfile = await _userProfileRepository.DbSet
            .Include(up => up.User)
            .Include(up => up.Team)
            .FirstOrDefaultAsync(up => up.Id == id);
        if (userProfile is null)
        {
            return NotFound($"UserProfile with id '{id}' does not exist.");
        }

        var team = await _teamRepository.GetAsync(teamId);
        if (team == null)
        {
            return NotFound($"Team with id '{teamId}' does not exist.");
        }
        
        userProfile.Team = team;

        var result = await _userProfileRepository.UpdateAsync(userProfile);
        if (result is null)
        {
            return NotFound("Error updating user profile in db");
        }

        var userProfileResponse = _mapper.Map<UserProfileResponse>(result);

        return Ok(userProfileResponse);
    }
}