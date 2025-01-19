using System.Security.Cryptography;
using System.Text;
using LaboRevision.BLL.Exceptions;
using LaboRevision.BLL.Mapper;
using LaboRevision.BLL.Models;
using LaboRevision.DAL.Repositories;

namespace LaboRevision.BLL.Services;

public class AuthService
{
    private readonly UserRepository _userRepository;

    public AuthService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? Login(User user)
    {
        string emailInput = user.Email.ToLower();
        User? userDb = this._userRepository.Login(emailInput)?.ToModel();

        if (userDb != null && userDb.Password == this.GenerateHash(emailInput, user.Password))
        {
            return userDb;
        }

        throw new LoginException();
    }

    public bool Register(User user)
    {
        user.Password = this.GenerateHash(user.Email, user.Password);
        user.Created = DateTime.UtcNow;
        
        return this._userRepository.Register(user.ToEntity());
    }
    
    private string GenerateHash(string email, string password)
    {
        using SHA256 sha256Hash = SHA256.Create();

        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes($"{email}:{password}"));

        StringBuilder builder = new StringBuilder();
        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}