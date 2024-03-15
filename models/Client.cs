namespace MenuMate.Models;

class Client
{
    public Guid Id { get; private set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Nume { get; set; }
    public string? Prenume { get; set; }

    public Client()
    {
        Id = Guid.NewGuid();
    }

    public override string ToString()
    {
        return $"Client {Id}[Email: '{Email}', Password (Encrypted): '{Password}', Nume: '{Nume}', Prenume: '{Prenume}']";
    }
}