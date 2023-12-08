using Microsoft.AspNetCore.Mvc;
using Neox.Common;
using Neox.Model;
using Neox.Services;

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
    [ProducesResponseType(typeof(IEnumerable<Client>), 200)]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _service.GetAll());
        }
        catch (BusinessException e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode((int)e.HttpStatusCode, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            return Ok(await _service.GetById(id));
        }
        catch (BusinessException e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode((int)e.HttpStatusCode, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost()]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<IActionResult> Create(Client client)
    {
        try
        {
            return Ok(await _service.Create(client));
        }
        catch (BusinessException e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode((int)e.HttpStatusCode, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> GetById(int id, Client client)
    {
        try
        {
            client.Id = id;
            await _service.Update(client);
            return Ok();
        }
        catch (BusinessException e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode((int)e.HttpStatusCode, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.Delete(new Client { Id = id });
            return Ok();
        }
        catch (BusinessException e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode((int)e.HttpStatusCode, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
}