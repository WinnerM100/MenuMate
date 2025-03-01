
namespace MenuMate.Models.DTOs;

public record UserDAO
{
    public Guid? Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public override string ToString()
    {
        return $"UserDAO[Id='{Id}', Email='{Email}', Password='{Password}']";
    }
}