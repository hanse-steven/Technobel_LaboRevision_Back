namespace LaboRevision.DTO;

public class CartItemDTO
{
    public ProductDTO Product { get; set; }
    public int Quantity { get; set; }
}

public class AddCartItemDTO
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }

    public override string ToString()
    {
        return $"productId: {Id}, quantity: {Quantity}";
    }
}