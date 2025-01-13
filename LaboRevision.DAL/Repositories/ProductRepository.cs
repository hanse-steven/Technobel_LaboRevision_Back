using Dapper;
using LaboRevision.DAL.Entities;
using Npgsql;

namespace LaboRevision.DAL.Repositories;

public class ProductRepository
{
    private readonly NpgsqlConnection _conn;
    public ProductRepository(NpgsqlConnection conn)
    {
        _conn = conn;
    }

    public IEnumerable<Product> GetAll()
    {
        const string query = @"
            SELECT
                ""id"",
                ""name"",
                ""price"",
                ""quantity""
            FROM ""product""
            ORDER BY ""quantity"" = 0, ""name"";
        ";
        return this._conn.Query<Product>(query);
    }
    
    public Product? GetById(Guid id)
    {
        const string query = @"
            SELECT
                ""id"",
                ""name"",
                ""price"",
                ""quantity""
            FROM ""product""
            WHERE ""id"" = @id;
        ";
        return this._conn.QuerySingleOrDefault<Product>(query, new { id });
    }
}











