namespace LaboRevision.DTO;

public class UserDTO
{
    public required string Email { get; set; }
    public DateTime Created { get; set; }
}

public class UserLoginDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UserRegisterDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}