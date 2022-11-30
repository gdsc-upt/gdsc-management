using AutoMapper;
using GdscManagement.API.Features.Users.Models;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.API.Features.Users;

[Route("api/users")]
[Authorize(Roles = Roles.Admin)]
public class UserController : ApiController<User, UserResponse>
{
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager, IMapper mapper) : base(mapper)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
    {
        var users = await _userManager.Users.ToListAsync();
        return Ok(Map(users));
    }
}
