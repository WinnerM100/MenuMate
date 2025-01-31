namespace MenuMate.Models;

public record UserDAO
{
    public Guid Id { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }

    public ICollection<Role> Roles{ get; set; }
}