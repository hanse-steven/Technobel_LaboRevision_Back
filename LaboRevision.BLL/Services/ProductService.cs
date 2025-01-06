using LaboRevision.BLL.Mapper;
using LaboRevision.BLL.Models;
using LaboRevision.DAL.Repositories;

namespace LaboRevision.BLL.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public IEnumerable<Product> GetProducts()
    {
        return _productRepository.GetAll().Select(p => p.ToModel());
    }
    
    public Product? GetProductById(Guid id)
    {
        return _productRepository.GetById(id)?.ToModel();
    }
    
    public Guid AddProduct(Product product)
    {
        return _productRepository.Add(product.ToEntity());
    }
    
    public bool UpdateProduct(Product product)
    {
        return _productRepository.Update(product.ToEntity());
    }
    
    public bool DeleteProduct(Guid id)
    {
        return _productRepository.Delete(id);
    }
}