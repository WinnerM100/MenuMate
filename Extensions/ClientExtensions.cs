using MenuMate.DTOs;
using MenuMate.Models;

namespace MenuMate.Extensions;

public static class ClientExtenstions
{
    public static Client AsClient(this ClientDTO clientDTO)
    {
        return new Client() with
        {
            Email = clientDTO.Email,
            Nume = clientDTO.Nume,
            Prenume = clientDTO.Prenume,
            Password = clientDTO.Password
        };
    }
}