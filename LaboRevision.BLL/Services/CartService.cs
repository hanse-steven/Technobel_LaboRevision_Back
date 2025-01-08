using LaboRevision.BLL.Mapper;
using LaboRevision.BLL.Models;
using LaboRevision.DAL.Repositories;

namespace LaboRevision.BLL.Services;

public class CartService
{
    private readonly CartRepository _cartRepository;
    private readonly ProductService _productService;
    public CartService(CartRepository cartRepository, ProductService productService)
    {
        _cartRepository = cartRepository;
        _productService = productService;
    }

    public Cart GetById(Guid session)
    {
        var cartItems = _cartRepository.GetBySession(session);
        var products = cartItems.ToDictionary(
            item => _productService.GetProductById(item.product)!,
            item => item.quantity
        );

        return new Cart
        {
            session = session,
            products = products
        };
    }

    public bool AddToCart(Guid session, Guid product, int quantity)
    {
        return _cartRepository.AddProduct(session, product, quantity);
    }

    public bool ModifyQuantityOfProduct(Guid session, Guid product, int quantity)
    {
        return _cartRepository.ModifyQuantityOfProduct(session, product, quantity);
    }
}