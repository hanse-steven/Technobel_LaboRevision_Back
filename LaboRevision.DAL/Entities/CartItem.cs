namespace LaboRevision.DAL.Entities;

public class CartItem
{
    public int id { get; set; }
    public Guid session { get; set; }
    public Guid product { get; set; }
    public int quantity { get; set; }
}