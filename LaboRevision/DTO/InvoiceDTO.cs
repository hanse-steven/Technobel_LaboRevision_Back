namespace LaboRevision.DTO;

public class InvoiceDTO
{
    public int InvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public IEnumerable<ProductInvoiceDTO> Products { get; set; }
}