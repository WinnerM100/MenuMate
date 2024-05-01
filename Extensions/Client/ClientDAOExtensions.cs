using MenuMate.DAOs;
using MenuMate.DTOs;
using MenuMate.Models;

namespace MenuMate.Extensions;

public static class ClientDAOExtensions
{
    public static ClientDAO AsClientDAO(this ClientDTO clientDTO)
    {
        return new ClientDAO() with
        {
            Id = clientDTO.Id == Guid.Empty?Guid.NewGuid():clientDTO.Id,
            Email = clientDTO.Email,
            Nume = clientDTO.Nume,
            Prenume = clientDTO.Prenume,
            Password = clientDTO.Password
        };
    }

    public static ClientDAO AsClientDAO(this Client client)
    {
        return new ClientDAO() with
        {
            Id = client.Id,
            Email = client.Email,
            Nume = client.Nume,
            Prenume = client.Prenume,
            Password = client.Password
        };
    }
}