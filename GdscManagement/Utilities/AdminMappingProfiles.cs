using AutoMapper;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Features.Users;

namespace GdscManagement.Utilities;

public class AdminMappingProfiles: Profile
{
    public AdminMappingProfiles()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
    }
}
