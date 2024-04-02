namespace MenuMate.DAOs;

public record ClientDAO
{
    public Guid? Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Nume { get; set; }
    public string? Prenume { get; set; }

    public override string ToString()
    {
        return $"Client {Id}[Email: '{Email}', Password (Encrypted): '{Password}', Nume: '{Nume}', Prenume: '{Prenume}']";
    }
}