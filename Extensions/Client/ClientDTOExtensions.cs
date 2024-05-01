using MenuMate.DTOs;
using MenuMate.Models;

namespace MenuMate.Extensions;

public static class ClientDTOExtenstions
{
    public static ClientDTO AsClientDTO(this Client client, bool sendId = false)
    {
        return new ClientDTO()
        {
            Id = sendId?client.Id:Guid.Empty,
            Email = client.Email,
            Nume = client.Nume,
            Prenume = client.Prenume,
            Password = client.Password
        };
    }
}