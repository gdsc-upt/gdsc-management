using AutoMapper;
using GdscManagement.Common.Features.Teams.Models;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Features.Roles;
using GdscManagement.Features.Teams;
using GdscManagement.Features.Users;

namespace GdscManagement.Utilities;

public class AdminMappingProfiles : Profile
{
    public AdminMappingProfiles()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<Role, RoleViewModel>().ReverseMap();
        CreateMap<Team, TeamViewModel>().ReverseMap();
        // CreateMap<Client, ClientViewModel>().ReverseMap();
        //transofrma un role in roleviewmodel
    }
}