using Neox.Model;

namespace Neox.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAll();

        Task<Client> GetById(int id);

        Task<int> Create(Client client);

        Task Update(Client client);

        Task Delete(Client client);
    }
}