using LaboRevision.BLL.Models;
using LaboRevision.DAL.Repositories;

namespace LaboRevision.BLL.Services;

public class InvoiceService
{
    private readonly InvoiceRepository _invoiceRepository;
    public InvoiceService(InvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public bool CreateInvoice(Guid session, string email)
    {
        return _invoiceRepository.CreateInvoice(session, email);
    }

    public IEnumerable<Invoice> GetByEmail(string email)
    {
        List<Invoice> invoices = [];
        foreach (var o in this._invoiceRepository.GetByEmail(email))
        {
            Invoice? invoice = invoices.FirstOrDefault(i => i.InvoiceId == o.InvoiceId);

            if (invoice == null)
            {
                invoice = new Invoice
                {
                    InvoiceId = o.InvoiceId,
                    InvoiceDate = o.InvoiceDate,
                    Products = []
                };
                invoices.Add(invoice);
            }

            invoice.Products.Add(new ProductShort
            {
                Name = o.ProductName,
                Quantity = o.ProductQuantity,
                Price = o.ProductPrice
            });
        }

        return invoices;
    }
}