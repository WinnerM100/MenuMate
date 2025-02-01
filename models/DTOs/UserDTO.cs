
namespace MenuMate.Models.DTOs;

public record UserDTO
{
    public Guid? Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public override string ToString()
    {
        return $"User[Id='{Id}', Email='{Email}', Password='{Password}']";
    }
}