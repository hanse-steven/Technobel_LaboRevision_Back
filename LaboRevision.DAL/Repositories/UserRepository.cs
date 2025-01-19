using Dapper;
using LaboRevision.DAL.Entities;
using Npgsql;

namespace LaboRevision.DAL.Repositories;

public class UserRepository
{
    private readonly NpgsqlConnection _conn;
    public UserRepository(NpgsqlConnection conn)
    {
        _conn = conn;
    } 
    
    public User? Login(string email)
    {
        const string query = @"
            SELECT
                ""email"",
                ""password"",
                ""created""
            FROM ""user""
            WHERE ""email"" = @Email;
        ";
        return this._conn.QuerySingleOrDefault<User>(query, new { Email = email });
    }

    public bool Register(User user)
    {
        const string query = @"
            INSERT INTO ""user""
            VALUES (
                @Email,
                @Password,
                @Created
            )
            RETURNING ""email"";
        ";
        return this._conn.QuerySingle<string>(query, new
        {
            user.Email,
            user.Password,
            user.Created
        }).Length > 0;
    }

}