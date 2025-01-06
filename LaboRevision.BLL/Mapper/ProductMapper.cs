using Entities = LaboRevision.DAL.Entities;
using Models = LaboRevision.BLL.Models;

namespace LaboRevision.BLL.Mapper;

public static class ProductMapper
{
    public static Models.Product ToModel(this Entities.Product entity)
    {
        return new Models.Product
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price,
            Quantity = entity.Quantity
        };
    }
    
    public static Entities.Product ToEntity(this Models.Product model)
    {
        return new Entities.Product
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Quantity = model.Quantity
        };
    }
}