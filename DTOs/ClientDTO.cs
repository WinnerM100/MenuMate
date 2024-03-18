namespace MenuMate.DTOs;

public record ClientDTO
{
    public Guid? Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Nume { get; set; }
    public string? Prenume { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ClientDTO()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        Id = Guid.NewGuid();
    }
}