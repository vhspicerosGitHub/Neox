namespace Neox.Repositories.SqlLite
{
    public class Queries
    {

        private static readonly string table = "clients";

        public static readonly string GetAll = $"SELECT id,name,email,deleted from {table} where deleted = 0";

        public static readonly string GetByEmail = $"select id,name,email,delete from {table} where email=@email";

        public static readonly string GetById = $"select id,name,email,delete from {table} where id=@id";

        public static readonly string Create = $@"
                            INSERT INTO {table} (name,email) VALUES (@name,@email);
                            SELECT last_insert_rowid();";

        public static readonly string Delete = "UPDATE {table} SET deleted = 1 WHERE id = @id";

        public static readonly string Update = @"
                            INSERT INTO {table} (name,email) VALUES (@name,@email);
                            SELECT last_insert_rowid();";



    }
}
