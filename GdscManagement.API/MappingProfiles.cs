using AutoMapper;
using GdscManagement.API.Features.Users.Models;
using GdscManagement.Common.Features.Users.Models;

namespace GdscManagement.API;

public class ApiMappingProfiles : Profile
{
    public ApiMappingProfiles()
    {
        CreateMap<User, UserResponse>().ReverseMap();
    }
}
