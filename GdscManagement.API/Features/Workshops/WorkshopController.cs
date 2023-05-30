using AutoMapper;
using GdscManagement.API.Features.Workshops.Models;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Features.Workshops.Models;
using GdscManagement.Common.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.API.Features.Workshops;

[ApiController]
[Route("api/[Controller]")]
public class WorkshopController : ControllerBase
{
    private readonly IRepository<Workshop> _workshopRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public WorkshopController(IRepository<Workshop> workshopRepository, IRepository<User> userRepository,
        IMapper mapper)
    {
        _workshopRepository = workshopRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<WorkshopResponse>> AddWorkshop(WorkshopRequest request)
    {
        var trainer = await _userRepository.GetAsync(request.TrainerId);
        if (trainer == null)
        {
            return BadRequest($" User with id '{request.TrainerId}' does not exist.");
        }

        var workshop = _mapper.Map<Workshop>(request);
        workshop.Trainer = trainer;
        var addedWorkshop = await _workshopRepository.AddAsync(workshop);
        var workshopResponse = _mapper.Map<WorkshopResponse>(addedWorkshop);

        return Ok(workshopResponse);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkshopResponse>>> GetWorkshops()
    {
        var workshops = await _workshopRepository.DbSet.Include(q => q.Trainer).ToListAsync();

        return Ok(workshops.Select(
            workshopResponses => _mapper.Map<WorkshopResponse>(workshopResponses)).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkshopResponse>> GetWorkshopById(string id)
    {
        var workshop = await _workshopRepository.DbSet
            .Include(q => q.Trainer)
            .Include(q => q.Participants)
            .FirstOrDefaultAsync(q => q.Id == id);
        if (workshop == null)
        {
            return NotFound($" Workshop with id '{id}' doesn't exist.");
        }

        return Ok(_mapper.Map<WorkshopResponse>(workshop));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<WorkshopResponse>> DeleteWorkshop(string id)
    {
        var deletedWorkshop = await _workshopRepository.DeleteAsync(id);
        if (deletedWorkshop == null)
        {
            return NotFound($" Workshop with id '{id}' doesn't exist.");
        }

        var workshopResponse = _mapper.Map<WorkshopResponse>(deletedWorkshop);
        return Ok(workshopResponse);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WorkshopResponse>> UpdateWorkshop(string id, WorkshopRequest request)
    {
        var workshop = await _workshopRepository.GetAsync(id);
        if (workshop == null)
        {
            return NotFound($" Workshop with id '{id}' doesn't exist.");
        }

        var trainer = await _userRepository.GetAsync(request.TrainerId);
        if (trainer == null)
        {
            return NotFound($" User with id '{id}' doesn't exist.");
        }

        var updatedWorksop = _mapper.Map(request, workshop);
        var result = await _workshopRepository.UpdateAsync(updatedWorksop);
        if (result == null)
            return BadRequest();
        var workshopResponse = _mapper.Map<WorkshopResponse>(result);
        return Ok(workshopResponse);
    }

    [HttpPut("{id}/trainer")]
    public async Task<ActionResult<WorkshopResponse>> ChangeTrainer(string id, string trainerid)
    {
        var workshop = await _workshopRepository.GetAsync(id);
        if (workshop == null)
        {
            return NotFound($" Workshop with id '{id}' doesn't exist.");
        }

        var trainer = await _userRepository.GetAsync(trainerid);
        if (trainer == null)
        {
            return NotFound($" User with id '{id}' doesn't exist.");
        }

        workshop.Trainer = trainer;
        var result = await _workshopRepository.UpdateAsync(workshop);
        if (result == null)
            return BadRequest();
        var workshopResponse = _mapper.Map<WorkshopResponse>(result);
        return Ok(workshopResponse);
    }

    [HttpGet("future")]
    public async Task<ActionResult<IEnumerable<WorkshopResponse>>> GetFutureWorkshops()
    {
        var workshops = await _workshopRepository.DbSet
            .Include(q => q.Trainer)
            .Where(q => q.DateStart > DateTime.UtcNow)
            .ToListAsync();
        return Ok(workshops.Select(
            workshopResponses => _mapper.Map<WorkshopResponse>(workshopResponses)));
    }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<WorkshopResponse>>> GetAvailableWorkshops()
    {
        var workshops = await _workshopRepository.DbSet
            .Include(q => q.Trainer)
            .Where(q => q.DateStart > DateTime.UtcNow && q.Participants.Count < q.MaxCapacity)
            .ToListAsync();
        return Ok(workshops.Select(
            workshopResposes => _mapper.Map<WorkshopResponse>(workshopResposes)));
    }

    [HttpGet("Past")]
    public async Task<ActionResult<IEnumerable<WorkshopResponse>>> GetPastWorkshops()
    {
        var workshops = await _workshopRepository.DbSet
            .Include(q => q.Trainer)
            .Where(q => q.DateStart < DateTime.UtcNow)
            .ToListAsync();
        return Ok(workshops.Select(
            workshopResponses => _mapper.Map<WorkshopResponse>(workshopResponses)));
    }

    [HttpPost("participant")]
    public async Task<ActionResult<WorkshopResponse>> AddParticipant(ParticipantRequest request)
    {
        var workshop = await _workshopRepository.DbSet.Include(q=>q.Participants).FirstOrDefaultAsync(p=>p.Id==request.WorkshopId);
        if (workshop == null)
            return NotFound($"Workshop with id '{request.WorkshopId}' doesn't exist.");

        if (workshop.OccupiedSeates >= workshop.MaxCapacity)
            return BadRequest("Occupied seates bigger than maxcap.");

        var user = await _userRepository.GetAsync(request.UserId);
        if (user == null)
            return NotFound($"User with id '{request.UserId}' doesn't exist.");

        workshop.Participants.Add(user);
        workshop.OccupiedSeates += 1;

        var result = await _workshopRepository.UpdateAsync(workshop);

        if (result == null)
            return BadRequest();

        var workshopResponse = _mapper.Map<WorkshopResponse>(result);
        return Ok(workshopResponse);
    }

    [HttpDelete("{id}/participant/{participantId}")]
    public async Task<ActionResult<WorkshopResponse>> RemoveParticipant(string id, string participantId)
    {
        var workshop = await _workshopRepository.DbSet.Include(w => w.Participants)
            .FirstOrDefaultAsync(w => w.Id == id);
        if (workshop == null)
            return NotFound($"Workshop with id '{id}' doesn't exist.");

        var participant = workshop.Participants.FirstOrDefault(p => p.Id == participantId);

        if (participant == null)
            return NotFound($"Participant with id '{participantId}' doesn't exist.");

        workshop.Participants.Remove(participant);
        var result = await _workshopRepository.UpdateAsync(workshop);
        if (result == null)
            return BadRequest();

        var workshopResponse = _mapper.Map<WorkshopResponse>(result);
        return Ok(workshopResponse);
    }
}