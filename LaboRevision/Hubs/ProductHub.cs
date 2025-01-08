using LaboRevision.BLL.Services;
using LaboRevision.DTO;
using LaboRevision.Mapper;
using Microsoft.AspNetCore.SignalR;

namespace LaboRevision.Hubs;

public class ProductHub : Hub
{
    private readonly ProductService _productService;
    private readonly ILogger<ProductHub> _logger;
    public ProductHub(ProductService productService, ILogger<ProductHub> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public async Task GetProducts()
    {
        IEnumerable<ProductDTO> products = _productService.GetProducts().Select(p => p.TotDTO());
        await Clients.Caller.SendAsync("GetProducts", products);
    }
}