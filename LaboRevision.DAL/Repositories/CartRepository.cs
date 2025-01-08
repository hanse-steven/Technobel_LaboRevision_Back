using Dapper;
using LaboRevision.DAL.Entities;
using Npgsql;

namespace LaboRevision.DAL.Repositories;

public class CartRepository
{
    private readonly NpgsqlConnection _conn;
    public CartRepository(NpgsqlConnection conn)
    {
        _conn = conn;
    }

    public IEnumerable<CartItem> GetBySession(Guid session)
    {
        const string query = @"
            SELECT
                ""id"",
                ""session"",
                ""product"",
                ""quantity""
            FROM ""cartitem"";
        ";
        return this._conn.Query<CartItem>(query);
    }
    
    public bool AddProduct(Guid session, Guid product, int quantity)
    {
        List<CartItem> cartItems = this.GetBySession(session).ToList();
        
        if (cartItems.Any(ci => ci.product == product))
        {
            const string query = @"
                UPDATE ""cartitem""
                SET ""quantity"" = ""quantity"" + @quantity
                WHERE ""session"" = @session AND ""product"" = @product;
            ";
            return this._conn.Execute(query, new { session, product, quantity }) > 0;
        }
        else
        {
            const string query = @"
                INSERT INTO ""cartitem"" (""session"", ""product"", ""quantity"")
                VALUES (@session, @product, @quantity);
            ";
            return this._conn.Execute(query, new { session, product, quantity }) > 0;
        }
    }
    
    public bool ModifyQuantityOfProduct(Guid session, Guid product, int quantity)
    {
        if (quantity == 0)
        {
            const string query = @"
                DELETE FROM ""cartitem""
                WHERE ""session"" = @session AND ""product"" = @product;
            ";
            return this._conn.Execute(query, new { session, product }) > 0;
        }
        else
        {
            const string query = @"
                UPDATE ""cartitem""
                SET ""quantity"" = @quantity
                WHERE ""session"" = @session AND ""product"" = @product;
            ";
            return this._conn.Execute(query, new { session, product, quantity }) > 0;
        }
    }
}