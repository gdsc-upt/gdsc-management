using AutoMapper;
using GdscManagement.API.Features.Projects.Models;
using GdscManagement.API.Features.Teams.Models;
using GdscManagement.Common.Features.Projects.Models;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Repository;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IMapper _mapper;


    public ProjectsController(IRepository<Project> projectRepository, IRepository<User> userRepository, IMapper mapper)

    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
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
            projectResponses => _mapper.Map<ProjectResponse>(projectResponses)).ToList());
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjectResponse>> GetProjectById(string id)
    {
        var project = await _projectRepository.DbSet.Include(q=>q.Manager).FirstOrDefaultAsync(p=>p.Id==id);

        if (project is null)
        {
            return NotFound("try again xoxo");
        }


        return Ok(_mapper.Map<ProjectResponse>(project)); //managerid=null ? 
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<ProjectResponse>> DeleteProject(string id)
    {
        var deletedProject = await _projectRepository.DeleteAsync(id);
        if (deletedProject is null)
        {
            return NotFound();
        }

        var projectResponse = _mapper.Map<ProjectResponse>(deletedProject);

        return Ok(projectResponse);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectResponse>> UpdateProject(string id, ProjectRequest request)
    {
        var project = await _projectRepository.GetAsync(id);
        if (project is null)
        {
            return NotFound();
        }

        var manager = await _userRepository.GetAsync(request.ManagerId);
        if (manager == null)
        {
            return BadRequest($"User with id '{request.ManagerId}' does not exist.");
        }


        var updatedProject = _mapper.Map(request, project);

        var result = await _projectRepository.UpdateAsync(updatedProject);
        if (result is null)
        {
            return BadRequest();
        }

        var projectResponse = _mapper.Map<ProjectResponse>(result);

        return Ok(projectResponse);
    }


    [HttpPut("{id}/manager")]
    public async Task<ActionResult<ProjectResponse>> ChangeManager(string id, string ManagerId)
    {
        var project = await _projectRepository.GetAsync(id);
        if (project is null)
        {
            return NotFound();
        }

        var manager = await _userRepository.GetAsync(ManagerId);

        if (manager == null)
        {
            return BadRequest($"User with id '{ManagerId}' does not exist.");
        }


        project.Manager = manager;

        var result = await _projectRepository.UpdateAsync(project);
        if (result is null)
        {
            return BadRequest();
        }

        var projectResponse = _mapper.Map<ProjectResponse>(result);

        return Ok(projectResponse);
    }


    [HttpPut("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjectResponse>> ChangeStatus(string id, ProjectStatuses newstatus)
    {
        var project = await _projectRepository.GetAsync(id);
        if (project is null)
        {
            return NotFound();
        }


        // ProjectStatuses status = await _projectRepository.GetAsync(newstatus);  //sper ca n am stricat getasync
        // if(status is null)

        // {
        // return BadRequest();
        // }

        project.Status = newstatus;

        await _projectRepository.SaveAsync();
        return Ok(project);
    }


    [HttpGet("OnGoing")]
    public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetOnGoingProjects(ProjectStatuses status)
    {
        var projects = await _projectRepository.DbSet.Include(q => q.Manager).Where(q => q.Status == status)
            .ToListAsync();

        return Ok(projects);
    }
    
    
}