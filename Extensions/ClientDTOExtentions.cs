
using MenuMate.Models;
using MenuMate.Models.DTOs;

namespace MenuMate.Extensions;

public static class ClientDTOExtensions
{
    public static Client ToClient (this ClientDTO dto, bool maskClientId = true)
    {
        return new Client
        {
            Id = (Guid)((dto.Id == null || dto.Id == Guid.Empty || maskClientId)? Guid.NewGuid(): dto.Id),
            Name = dto.Name,
            Prenume = dto.Prenume
        };
    }
}