namespace LaboRevision.BLL.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class ProductShort
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}