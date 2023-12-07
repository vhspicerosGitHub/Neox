using Microsoft.AspNetCore.Mvc;
using Neox.Model;
using Neox.Services;
using System.Reflection.Metadata.Ecma335;

namespace Neox.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly IClientService _service;
    public ClientController(ILogger<ClientController> logger, IClientService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet()]
    public async Task<IEnumerable<Client>> Get()
    {
        return await _service.GetAll();
    }

    [HttpPost()]
    public async Task<int> Create(Client client)
    {
        return await _service.Create(client);
    }


    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        await _service.Delete(new Client { Id = id });
    }
}