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
}