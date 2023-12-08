using Microsoft.Extensions.Logging;
using Neox.Common;
using Neox.Model;
using Neox.Repositories;
using System.Net;

namespace Neox.Services;

public class ClientService : IClientService
{
    private readonly ILogger<ClientService> _logger;
    private readonly IClientRepository _repository;
    public ClientService(ILogger<ClientService> logger, IClientRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public Task<int> Create(Client client)
    {
        if (_repository.GetByEmail(client.Email) != null)
            throw new BusinessException("Ya existe un cliente con ese correo");

        if (string.IsNullOrWhiteSpace(client.Email))
            throw new BusinessException("El Email no puede ser vacio");

        if (string.IsNullOrWhiteSpace(client.Name))
            throw new BusinessException("El Nombre no puede ser vacio");

        return _repository.Create(client);
    }

    public Task Delete(Client client)
    {
        if (_repository.GetById(client.Id) == null)
            throw new BusinessException("El cliente no existe", HttpStatusCode.NotFound);
        return _repository.Delete(client);
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Client> GetById(int id)
    {
        var client = await _repository.GetById(id);
        if (client == null)
            throw new BusinessException("El cliente no existe", HttpStatusCode.NotFound);
        var i = Convert.ToInt32("hola");
        return client;
    }

    public async Task Update(Client client)
    {
        if (await _repository.GetById(client.Id) == null)
            throw new BusinessException("El cliente no existe", HttpStatusCode.NotFound);
        await _repository.Update(client);
    }
}