using LaboRevision.BLL.Services;
using LaboRevision.DTO;
using LaboRevision.Hubs;
using LaboRevision.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LaboRevision.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly IHubContext<CartHub> _cartHub;
    public CartController(ProductService productService, IHubContext<CartHub> cartHub)
    {
        _productService = productService;
        _cartHub = cartHub;
    }

    private IEnumerable<CartItemDTO> GetCart()
    {
        IEnumerable<CartItemDTO>? c = HttpContext.Session.Get<IEnumerable<CartItemDTO>>("cart");
        if (c is null)
        {
            c = [];
            HttpContext.Session.Set("cart", []);
        }
        return c;
    }
    

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CartItemDTO>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get()
    {
        IEnumerable<CartItemDTO> cart = this.GetCart();
        return this.Ok(cart);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult AddToCart(AddCartItemDTO aci)
    {
        try
        {
            IEnumerable<CartItemDTO> cart = this.GetCart();
            bool isAddToCart = false;

            for (int i = 0; i < cart.Count() && !isAddToCart; i++)
            {
                if (cart.ElementAt(i).Product.Id == aci.Id)
                {
                    cart.ElementAt(i).Quantity += aci.Quantity;
                    isAddToCart = true;
                }
            }
        
            if (!isAddToCart)
            {
                ProductDTO? p = this._productService.GetProductById(aci.Id)?.ToDTO();
            
                if (p is null)
                    return this.BadRequest($"Product not found {aci}");
            
                cart = cart.Append(new CartItemDTO
                {
                    Product = p,
                    Quantity = aci.Quantity
                });
            }

            this._cartHub.Clients.All.SendAsync("CartUpdated", cart);
            return this.Created();
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        
    }
}
