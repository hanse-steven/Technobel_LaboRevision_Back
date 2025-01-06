using Models = LaboRevision.BLL.Models;

namespace LaboRevision.Mapper;

public static class ProductMapper
{
    public static DTO.ProductDTO ToDTO(this Models.Product model)
    {
        return new DTO.ProductDTO
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Quantity = model.Quantity
        };
    }
}