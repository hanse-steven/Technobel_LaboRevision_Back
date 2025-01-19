namespace LaboRevision.BLL.Models;

public class Invoice
{
    public int InvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public List<ProductShort> Products { get; set; }
}