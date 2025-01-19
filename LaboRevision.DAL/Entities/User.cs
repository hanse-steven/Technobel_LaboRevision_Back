namespace LaboRevision.DAL.Entities;

public class User
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public DateTime Created { get; set; }
}