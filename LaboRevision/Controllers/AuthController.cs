using LaboRevision.BLL.Exceptions;
using LaboRevision.BLL.Services;
using LaboRevision.DTO;
using LaboRevision.Mapper;
using LaboRevision.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaboRevision.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly JwtService _jwtService;
    public AuthController(AuthService authService, JwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }


    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Login([FromBody] UserLoginDTO userLoginDto)
    {
        try
        {
            UserDTO? user = this._authService.Login(userLoginDto.ToModel())?.ToDTO();

            if (user != null)
            {
                string token = this._jwtService.GenerateToken(user);
                return this.Ok(new { token });
            }

            throw new LoginException();
        }
        catch (LoginException e)
        {
            return this.BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    public IActionResult Register([FromBody] UserRegisterDTO userRegisterDto)
    {
        try
        {
            if (userRegisterDto.Password != userRegisterDto.ConfirmPassword)
                return this.BadRequest("Les mots de passes ne sont identiques");

            if (!this._authService.Register(userRegisterDto.ToModel()))
                throw new Exception("Erreur inconnue");

            return this.Created();
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
