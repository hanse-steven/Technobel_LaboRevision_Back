using LaboRevision.DTO;
using Models = LaboRevision.BLL.Models;

namespace LaboRevision.Mapper;

public static class ProductMapper
{
    public static DTO.ProductDTO TotDTO(this Models.Product model)
    {
        return new DTO.ProductDTO
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Quantity = model.Quantity
        };
    }
    
    public static DTO.ProductShortDTO ToShortDTO(this Models.Product model)
    {
        return new DTO.ProductShortDTO
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
        };
    }

    public static DTO.ProductInvoiceDTO ToDTO(this Models.ProductShort model)
    {
        return new ProductInvoiceDTO
        {
            Name = model.Name,
            Price = model.Price,
            Quantity = model.Quantity
        };
    }
}