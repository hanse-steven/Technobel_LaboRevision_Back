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

    private IEnumerable<ProductDTO> Products => _productService.GetProducts().Select(p => p.TotDTO());

    public async Task RefreshCartFromAll()
    {
        await Clients.All.SendAsync("GetProducts", Products);
    }

    public async Task GetProducts()
    {
        await Clients.Caller.SendAsync("GetProducts", Products);
    }
}