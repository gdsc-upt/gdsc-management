using AutoMapper;
using GdscManagement.API.Features.Projects.Models;
using GdscManagement.Common.Features.Projects.Models;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.API.Features.Projects;



[ApiController]
[Route("api/[Controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Developers> _devRepository;
    private readonly IMapper _mapper;


    public ProjectsController(IRepository<Project> projectRepository, IRepository<User> userRepository,
        IRepository<Developers> devRepository, IMapper mapper)

    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _devRepository = devRepository;
        _mapper = mapper;
    }




    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<ActionResult<ProjectResponse>> AddProject(ProjectRequest request)
    {

        var manager = await _userRepository.GetAsync(request.ManagerId);

        if (manager == null)
        {
            return BadRequest("nu merge bn");
            
        }

        var project = _mapper.Map<Project>(request);
        
        project.Manager = manager;

        var addedProject = await _projectRepository.AddAsync(project);
        var projectResponse = _mapper.Map<ProjectResponse>(addedProject);

        return Ok(projectResponse);
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    
    
    public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetProjects()

    {
        var projects = await _projectRepository.DbSet.Include(q => q.Manager).ToListAsync();

        return Ok(projects.Select(
            projectResponses => _mapper.Map<ProjectResponse> (projectResponses)).ToList());
        
        
    }

}