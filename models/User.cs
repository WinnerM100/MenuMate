
namespace MenuMate.Models;

public class User
{
    public Guid Id { get; set; }
    public Guid? ClientId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Client Client { get; set; }
    public ICollection<Role> Roles { get; } = new List<Role>();

    public override string ToString()
    {
        return $"User[Id='{Id}', Email='{Email}', Password='{Password}']";
    }
}