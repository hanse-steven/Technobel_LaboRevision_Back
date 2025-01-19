namespace LaboRevision.DAL.Entities;

public class Invoice
{
    public int InvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public required string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductQuantity { get; set; }
}