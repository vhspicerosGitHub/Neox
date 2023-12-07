using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Neox.Model;
using Dapper;

namespace Neox.Repositories
{
    public class ClientSqlLiteRepository : IClientRepository
    {
        private readonly ILogger<ClientSqlLiteRepository> _logger;
        private readonly string _connectionString = "Data Source=Database\\Database.sqlite";

        public ClientSqlLiteRepository(ILogger<ClientSqlLiteRepository> logger)
        {
            _logger = logger;
        }

        public async Task<int> Create(Client client)
        {
            var query = @"
                            INSERT INTO clients (name,email) VALUES (@name,@email);
                            SELECT last_insert_rowid();";
            using (var connection = new SqliteConnection(_connectionString))
            {
                var id = await connection.ExecuteScalarAsync<int>(query, new { name = client.Name, email = client.Email });
                return id;
            }
        }



        public async Task<IEnumerable<Client>> GetAll()
        {
            var query = "SELECT id,name,email,deleted from clients";
            using (var connection = new SqliteConnection(_connectionString))
            {
                var clients = await connection.QueryAsync<Client>(query);
                return clients;
            }
        }


        public async Task<Client> GetByEmail(string email)
        {
            var query = @"select id,name,email,delete from clients where email=@email";
            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstAsync<Client>(query, new { email });
            }
        }

        public async Task<Client> GetById(int id)
        {
            var query = @"select id,name,email,delete from clients where id = @id";
            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstAsync<Client>(query, new { id });
            }
        }


        public async Task Delete(Client client)
        {
            var query = @"UPDATE clients SET deleted=1 WHERE id = @id";
            _ = await new SqliteConnection(_connectionString).ExecuteAsync(query, new { id = client.Id });
        }

        public async Task Update(Client client)
        {
            var query = @"UPDATE clients SET name=@name,email=@email WHERE id = @id";
            _ = await new SqliteConnection(_connectionString).ExecuteAsync(query, new { id = client.Id, name = client.Name, email = client.Email });
        }
    }
}