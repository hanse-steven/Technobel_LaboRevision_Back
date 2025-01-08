using LaboRevision.BLL.Services;
using LaboRevision.DTO;
using LaboRevision.Mapper;
using LaboRevision.Utils;
using Microsoft.AspNetCore.SignalR;

namespace LaboRevision.Hubs;

public class CartHub : Hub
{
    private readonly CartService _cartService;
    private readonly ILogger<CartHub> _logger;
    public CartHub(CartService cartService, ILogger<CartHub> logger)
    {
        _cartService = cartService;
        _logger = logger;
    }

    private async Task RefreshCart()
    {
        CartDTO cart = _cartService.GetById(Context.GetSession()).ToDTO();
        await Clients.Caller.SendAsync("GetCart", cart.products);
    }
    
    public async Task GetCart()
    {
        await RefreshCart();
    }
    
    public async Task AddToCart(Guid product, int quantity)
    {
        _cartService.AddToCart(Context.GetSession(), product, quantity);
        _logger.LogInformation("Product {product} added to cart", product);
        await RefreshCart();
    }
    
    public async Task ModifyQuantityOfProduct(Guid product, int quantity)
    {
        _cartService.ModifyQuantityOfProduct(Context.GetSession(), product, quantity);
        _logger.LogInformation("Quantity of product {product} modified to {quantity}", product, quantity);
        await RefreshCart();
    }
}