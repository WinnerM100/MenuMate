
using MenuMate.Models;
using MenuMate.Models.DTOs;

namespace MenuMate.Extensions;

public static class ClientDTOExtensions
{
    public static Client ToClient (this ClientDTO dto, bool maskClientId = true)
    {
        return new Client
        {
            Id = (Guid)(maskClientId?Guid.Empty:((dto.Id == null || dto.Id == Guid.Empty)? Guid.Empty: dto.Id)),
            Name = dto.Name,
            Prenume = dto.Prenume,
            User = dto.UserDTO.ToUser() ?? null
        };
    }
}