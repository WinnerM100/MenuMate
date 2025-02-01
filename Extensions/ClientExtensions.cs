
using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

namespace MenuMate.Extensions;

public static class ClientExtensions
{
    public static ClientDTO ToClientDTO (this Client client, bool maskClientId = true)
    {
        return new ClientDTO
        {
            Id = maskClientId ? Guid.Empty : client.Id,
            Name = client.Name,
            Prenume = client.Prenume
        };
    }

    public static ClientDAO ToClientDAO (this Client client, bool maskClientId = true)
    {
        return new ClientDAO
        {
            Id = maskClientId ? Guid.Empty : client.Id,
            Name = client.Name,
            Prenume = client.Prenume
        };
    }
}