namespace LaboRevision.BLL.Models;

public class Cart
{
    public Guid session { get; set; }
    public Dictionary<Product, int> products { get; set; }
}