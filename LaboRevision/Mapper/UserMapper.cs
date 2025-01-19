using Models = LaboRevision.BLL.Models;
using DTO = LaboRevision.DTO;

namespace LaboRevision.Mapper;

public static class UserMapper
{
    public static Models.User ToModel(this DTO.UserLoginDTO dto)
    {
        return new Models.User
        {
            Email = dto.Email,
            Password = dto.Password
        };
    }

    public static DTO.UserDTO ToDTO(this Models.User model)
    {
        return new DTO.UserDTO
        {
            Email = model.Email,
            Created = model.Created
        };
    }

    public static Models.User ToModel(this DTO.UserRegisterDTO dto)
    {
        return new Models.User
        {
            Email = dto.Email,
            Password = dto.Password
        };
    }
}