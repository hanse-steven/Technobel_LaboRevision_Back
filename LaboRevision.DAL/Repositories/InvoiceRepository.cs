using System.Data;
using Dapper;
using Npgsql;

namespace LaboRevision.DAL.Repositories;

public class InvoiceRepository
{
    private readonly NpgsqlConnection _conn;
    public InvoiceRepository(NpgsqlConnection conn)
    {
        _conn = conn;
    }
    
    public bool CreateInvoice(Guid session, string email)
    {
        return this._conn.Execute(@"CALL CreateInvoice(@session,@email)", new
        {
            session = session,
            email = email
        }) > 0;
    }
}