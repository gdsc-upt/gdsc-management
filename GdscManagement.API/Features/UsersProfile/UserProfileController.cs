using AutoMapper;
using GdscManagement.API.Features.UsersProfile.Models;
using GdscManagement.Common.Features.UserProfile.Models;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.API.Features.UsersProfile;

[Route("api/usersprofile")]
public class UserProfileController : ControllerBase
{
    // private readonly UserManager<UserProfile> _userProfileManager;
    private readonly IRepository<UserProfile> _userProfileRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserProfileController(IRepository<UserProfile> userProfileRepository, IRepository<User> userRepository, IMapper mapper)
    {
        // _userProfileManager = userProfileManager;
        _userProfileRepository = userProfileRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserProfileResponse>> AddTeam(UserProfileRequest request)
    {
        var user = await _userRepository.GetAsync(request.UserId).ConfigureAwait(false);
        if (user == null)
        {
            return NotFound($"User with id '{request.UserId}' does not exist.");
        }
        // TODO: same for team

        var userProfile = _mapper.Map<UserProfile>(request);
        userProfile.User = user;

        var addedUserProfile = await _userProfileRepository.AddAsync(userProfile).ConfigureAwait(false);
        var userProfileResponse = _mapper.Map<UserProfileResponse>(addedUserProfile);

        return Ok(userProfileResponse);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserProfileResponse>>> GetTeams()
    {
        var userProfiles = await _userProfileRepository.GetAsync();
        var userProfileResponses = _mapper.Map<IEnumerable<UserProfileResponse>>(userProfiles);

        return Ok(userProfileResponses);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserProfileResponse>> GetTeamById(string id)
    {
        var userProfile = await _userProfileRepository.GetAsync(id);
        if (userProfile is null)
        {
            return NotFound("User Profile Not Found");
        }

        var userProfileResponse = _mapper.Map<UserProfileResponse>(userProfile);

        return Ok(userProfileResponse);
    }
    
    // [HttpPost("{id}/roles")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileResponse))]
    // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<IdentityError>))]
    // [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedHttpResult))]
    // public async Task<Results<BadRequest<IEnumerable<IdentityError>>, Ok<UserProfileResponse>>> AddRoles([FromBody] List<string> roles, [FromRoute] string id)
    // {
    //     var user = await _repository.AddAsync();
    //     
    //     // if (user is null)
    //     // {
    //     //     var error = new List<IdentityError> { new() { Description = "User not found", Code = "UserNotFound" } };
    //     //     return TypedResults.BadRequest(error.AsEnumerable());
    //     // }
    //
    //     var result = await _userManager.AddToRolesAsync(user, roles);
    //     return result.Succeeded ? TypedResults.Ok(Map(user)) : TypedResults.BadRequest(result.Errors);
    // }
}