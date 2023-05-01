using AutoMapper;
using GdscManagement.API.Features.Clients.Models;
using GdscManagement.API.Features.Projects.Models;
using GdscManagement.API.Features.Teams.Models;
using GdscManagement.API.Features.Users.Models;
using GdscManagement.API.Features.Workshops.Models;
using GdscManagement.Common.Features.Clients.Models;
using GdscManagement.Common.Features.Projects.Models;
using GdscManagement.Common.Features.Teams.Models;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Features.Workshops.Models;

namespace GdscManagement.API;

public class ApiMappingProfiles : Profile
{
    public ApiMappingProfiles()
    {
        CreateMap<User, UserResponse>().ReverseMap();
        
        CreateMap<Team, TeamResponse>().ReverseMap();
        
        CreateMap<Team, TeamRequest>().ReverseMap();
        
        CreateMap<Project, ProjectResponse>().ReverseMap();
        
        CreateMap<Project, ProjectRequest>().ReverseMap();

        CreateMap<Workshop, WorkshopResponse>().ReverseMap();

        CreateMap<Workshop, WorkshopRequest>().ReverseMap();

        CreateMap<Participants, ParticipantResponse>().ReverseMap();

        CreateMap<Participants, ParticipantRequest>().ReverseMap();

        CreateMap<Client, ClientRequest>().ReverseMap();

        CreateMap<Client, ClientResponse>().ReverseMap();
    }
}