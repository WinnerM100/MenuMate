using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MenuMate.DAOs;
using MenuMate.DTOs;
using Microsoft.VisualBasic;

namespace MenuMate.Models;

public record Client
{
    [Key]
    public Guid Id { get; private set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Nume { get; set; }
    public string? Prenume { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Client()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        Id = Guid.NewGuid();
    }

    public Client(ClientDAO clientDAO)
    {
        this.Id = clientDAO.Id == Guid.Empty?(Guid)clientDAO.Id:Guid.NewGuid();
#pragma warning disable CS8601 // Possible null reference assignment.
        this.Email = clientDAO.Email;
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
        this.Password = clientDAO.Password;
#pragma warning restore CS8601 // Possible null reference assignment.
        this.Nume = clientDAO.Nume;
        this.Prenume = clientDAO.Prenume;

#pragma warning disable CS8601 // Possible null reference assignment.
        this.User = new User()
        {
            Email = this.Email,
            Password = this.Password,
            Roles = Enumerable.Empty<Role>().ToList()
        };
#pragma warning restore CS8601 // Possible null reference assignment.
    }

    public Client(ClientDAO clientDAO, Role role)
    {
        this.Id = clientDAO.Id == Guid.Empty?(Guid)clientDAO.Id:Guid.NewGuid();
#pragma warning disable CS8601 // Possible null reference assignment.
        this.Email = clientDAO.Email;
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
        this.Password = clientDAO.Password;
#pragma warning restore CS8601 // Possible null reference assignment.
        this.Nume = clientDAO.Nume;
        this.Prenume = clientDAO.Prenume;

#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
        this.User = new User()
        {
            Email = this.Email,
            Password = this.Password,
            Roles = new []{ role }
        };
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8601 // Possible null reference assignment.
    }

     public Client(ClientDTO clientDTO)
    {
        this.Id = clientDTO.Id != Guid.Empty ? (Guid)clientDTO.Id : Guid.NewGuid();
#pragma warning disable CS8601 // Possible null reference assignment.
        this.Email = clientDTO.Email;
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
        this.Password = clientDTO.Password;
#pragma warning restore CS8601 // Possible null reference assignment.
        this.Nume = clientDTO.Nume;
        this.Prenume = clientDTO.Prenume;
    }

    public override string ToString()
    {
        return $"Client {Id}[Email: '{Email}', Password (Encrypted): '{Password}', Nume: '{Nume}', Prenume: '{Prenume}']";
    }
}