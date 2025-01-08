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
}