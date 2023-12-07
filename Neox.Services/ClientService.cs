using Microsoft.Extensions.Logging;
using Neox.Model;
using Neox.Repositories;

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
        return _repository.Create(client);
    }

    public Task Delete(Client client)
    {
        return _repository.Delete(client);
    }

    public Task<IEnumerable<Client>> GetAll()
    {
        return _repository.GetAll();
    }

    public Task<Client> GetById(int id)
    {
        return _repository.GetById(id);
    }

    public Task Update(Client client)
    {
        throw new NotImplementedException();
    }
}