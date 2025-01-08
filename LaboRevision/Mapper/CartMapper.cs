using DTO = LaboRevision.DTO;
using Models = LaboRevision.BLL.Models;

namespace LaboRevision.Mapper;

public static class CartMapper
{
    public static DTO.CartDTO ToDTO(this Models.Cart cart)
    {
        return new DTO.CartDTO
        {
            products = cart.products.ToDictionary(p => p.Key.ToShortDTO(), p => p.Value)
        };
    }
}