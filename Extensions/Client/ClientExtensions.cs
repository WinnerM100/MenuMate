using MenuMate.DAOs;
using MenuMate.DTOs;
using MenuMate.Models;

namespace MenuMate.Extensions;

public static class ClientExtenstions
{
    public static Client AsClient(this ClientDTO clientDTO)
    {
        return new Client(clientDTO);
    }

    public static Client AsClient(this ClientDAO clientDAO)
    {
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
        return new Client() with
        {
            Email = clientDAO.Email,
            Nume = clientDAO.Nume,
            Prenume = clientDAO.Prenume,
            Password = clientDAO.Password
        };
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8601 // Possible null reference assignment.
    }
}