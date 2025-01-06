using LaboRevision.DAL.Entities;

namespace LaboRevision.DAL.Repositories;

public class ProductRepository
{
    private List<Product> _products =
    [
        new Product {Id = Guid.Parse("7406b2cc-2c22-4157-9fba-d56a969bde7c"), Name = "Pizza", Price = 10, Quantity = 8},
        new Product {Id = Guid.Parse("eae4e338-a230-46ae-b29b-357053b7627e"), Name = "Burger", Price = 5, Quantity = 7},
        new Product {Id = Guid.Parse("2d764f53-9371-4bbd-b9f3-31e85dec8a6d"), Name = "Pâte", Price = 8, Quantity = 6},
        new Product {Id = Guid.Parse("8d9164eb-99c9-421d-b603-7c2c95ff8dae"), Name = "Salade", Price = 4, Quantity = 5},
        new Product {Id = Guid.Parse("e769faad-6a86-44cc-a87f-78efb9a5a6e8"), Name = "Sushi", Price = 12, Quantity = 4},
        new Product {Id = Guid.Parse("16de1a34-e5cb-4b5a-9197-5be3b4eaeee6"), Name = "Tacos", Price = 6, Quantity = 3},
        new Product {Id = Guid.Parse("a8896983-47ec-4daf-a00f-d174d43e10a2"), Name = "Frite", Price = 3, Quantity = 2},
        new Product {Id = Guid.Parse("6b7ea087-e29b-4cbd-adba-86113c232ef7"), Name = "Glace", Price = 2, Quantity = 1},
        new Product {Id = Guid.Parse("fe1f7583-5d30-413e-8f7c-230368470af3"), Name = "Donuts", Price = 1, Quantity = 0},
        new Product {Id = Guid.Parse("2d935bff-c81d-44d6-8f39-8d154d389ac7"), Name = "Cupcake", Price = 1, Quantity = 9}
    ];
    
    public IEnumerable<Product> GetAll()
    {
        return _products;
    }
    
    public Product? GetById(Guid id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }
    
    public Guid Add(Product product)
    {
        _products.Add(product);
        return product.Id;
    }
    
    public bool Update(Product product)
    {
        try
        {
            int index = _products.FindIndex(p => p.Id == product.Id);
            if (index == -1) return false;
            
            _products[index] = product;
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public bool Delete(Guid id)
    {
        return _products.RemoveAll(p => p.Id == id) > 0;
    }
}











