using MenuMate.Models;

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

    public bool Equals(Client obj)
    {
        if (obj == null)
            return false;
        if(this.Id != obj.Id)
            return false;
        if(!string.IsNullOrEmpty(this.Email) && !this.Email.Equals(obj.Email))
            return false;
        if(!string.IsNullOrEmpty(this.Password) && !this.Password.Equals(obj.Password))
            return false;
        if(!string.IsNullOrEmpty(this.Nume) && !this.Nume.Equals(obj.Nume))
            return false;
        if(!string.IsNullOrEmpty(this.Prenume) && !this.Prenume.Equals(obj.Prenume))
            return false;

        return true;
    }
}