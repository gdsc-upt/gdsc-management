using AutoMapper;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Features.Roles;
using GdscManagement.Features.Users;

namespace GdscManagement.Utilities;

public class AdminMappingProfiles: Profile
{
    public AdminMappingProfiles()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<Role, RoleViewModel>().ReverseMap(); //transofrma un role in roleviewmodel
    }
}
