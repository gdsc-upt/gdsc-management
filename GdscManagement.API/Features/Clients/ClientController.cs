using AutoMapper;
using GdscManagement.API.Features.Clients.Models;
using GdscManagement.Common.Features.Clients.Models;
using GdscManagement.Common.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GdscManagement.API.Features.Clients;

[ApiController]
[Route("api/[Controller]")]
public class ClientController : ControllerBase
{
    private readonly IRepository<Client> _clientRepository;
    private readonly IMapper _mapper;

    public ClientController(IRepository<Client> clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<ClientResponse>> AddClient(ClientRequest request)
    {
        var client = _mapper.Map<Client>(request);
        var addedClient = await _clientRepository.AddAsync(client);
        var clientResponse = _mapper.Map<ClientResponse>(addedClient);
        return Ok(clientResponse);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientResponse>>> GetClients()
    {
        var clients = await _clientRepository.GetAsync();
        return Ok(clients.Select(
            clientResponses => _mapper.Map<ClientResponse>(clientResponses)).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientResponse>> GetClient(string id)
    {
        var client = await _clientRepository.GetAsync(id);
        if (client == null)
            return NotFound($"Client with id '{id}' not found");
        
        var clientResponse = _mapper.Map<ClientResponse>(client);
        return Ok(clientResponse);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClientResponse>> ModifyClient(string id, ClientRequest request)
    {
        var client = await _clientRepository.GetAsync(id);
        if (client == null)
            return NotFound($"Client with id '{id}' not found");

        var updatedClient = _mapper.Map(request,client);
        var result = await _clientRepository.UpdateAsync(updatedClient);
        if (result == null)
            return BadRequest();

        return Ok(_mapper.Map<ClientResponse>(result));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ClientResponse>> DeleteClient(string id)
    {
        var client = await _clientRepository.GetAsync(id);
        if (client == null)
            return NotFound($"Client with id '{id}' not found");

        var deletedClient = await _clientRepository.DeleteAsync(id);

        return Ok(_mapper.Map<ClientResponse>(deletedClient));
    }
}