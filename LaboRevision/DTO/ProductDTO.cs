namespace LaboRevision.DTO;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class ProductShortDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class ProductInvoiceDTO
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}