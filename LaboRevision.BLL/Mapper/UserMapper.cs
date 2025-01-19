using Entities = LaboRevision.DAL.Entities;
using Models = LaboRevision.BLL.Models;

namespace LaboRevision.BLL.Mapper;

public static class UserMapper
{
    public static Models.User ToModel(this Entities.User entity)
    {
        return new Models.User
        {
            Email = entity.Email,
            Password = entity.Password,
            Created = entity.Created
        };
    }

    public static Entities.User ToEntity(this Models.User model)
    {
        return new Entities.User
        {
            Email = model.Email,
            Password = model.Password,
            Created = model.Created
        };
    }
}