using GdscManagement.API.Features.Users.Models;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.API.Features.Users;

[Route("api/users")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
public class UserController : ApiController<User, UserResponse>
{
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedHttpResult))]
    public async Task<Results<Ok<IEnumerable<UserResponse>>, UnauthorizedHttpResult>> Get()
    {
        var users = await _userManager.Users.ToListAsync();
        var response = users.Select(user =>
        {
            var res = Map(user);
            res.Roles = _userManager.GetRolesAsync(user).Result.ToList();
            return res;
        });
        return TypedResults.Ok(response);
    }

    [HttpPost("{id}/roles")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<IdentityError>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedHttpResult))]
    public async Task<Results<BadRequest<IEnumerable<IdentityError>>, Ok<UserResponse>>> AddRoles([FromBody] List<string> roles, [FromRoute] string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            var error = new List<IdentityError> { new() { Description = "User not found", Code = "UserNotFound" } };
            return TypedResults.BadRequest(error.AsEnumerable());
        }

        var result = await _userManager.AddToRolesAsync(user, roles);
        return result.Succeeded ? TypedResults.Ok(Map(user)) : TypedResults.BadRequest(result.Errors);
    }
}
