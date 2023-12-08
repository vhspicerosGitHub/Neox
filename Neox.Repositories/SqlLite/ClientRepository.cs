using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Neox.Model;
using Dapper;

namespace Neox.Repositories.SqlLite
{
    public class ClienteRepository : IClientRepository
    {
        private readonly ILogger<ClienteRepository> _logger;
        private readonly string _connectionString = "Data Source=Database\\Database.sqlite";

        public ClienteRepository(ILogger<ClienteRepository> logger)
        {
            _logger = logger;
        }

        public async Task<int> Create(Client client)
        {
            return await new SqliteConnection(_connectionString).ExecuteScalarAsync<int>(Queries.Create,
                new { name = client.Name, email = client.Email });
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await new SqliteConnection(_connectionString).QueryAsync<Client>(Queries.GetAll);
        }

        public async Task<Client?> GetByEmail(string email)
        {
            return await new SqliteConnection(_connectionString).QueryFirstOrDefaultAsync<Client>(Queries.GetByEmail,
                new { email });
        }

        public async Task<Client?> GetById(int id)
        {
            return await new SqliteConnection(_connectionString).QueryFirstOrDefaultAsync<Client>(Queries.GetById, new { id });
        }

        public async Task Delete(Client client)
        {
            _ = await new SqliteConnection(_connectionString).ExecuteAsync(Queries.Delete, new { id = client.Id });
        }

        public async Task Update(Client client)
        {
            _ = await new SqliteConnection(_connectionString).ExecuteAsync(Queries.Update,
                new { id = client.Id, name = client.Name, email = client.Email });
        }
    }
}