using System.Data;
using Dapper;
using LaboRevision.DAL.Entities;
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

    public IEnumerable<Invoice> GetByEmail(string email)
    {
        const string query = @"
            SELECT
                ""invoice"".""id"" AS InvoiceId,
                ""invoice"".""date"" AS InvoiceDate,
                ""product"".""name"" AS ProductName,
                ""item"".""price"" AS ProductPrice,
                ""item"".""quantity"" AS ProductQuantity
            FROM ""invoice""
            INNER JOIN ""invoiceitem"" item ON ""invoice"".""id"" = item.""invoice""
            INNER JOIN ""product"" ON item.""product"" = product.""id""
            WHERE ""invoice"".""user"" LIKE @email;
        ";
        return this._conn.Query<Invoice>(query, new
        {
            email
        });
    }
}