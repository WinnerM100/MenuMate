using MenuMate.Models;
using MenuMate.Models.DAOs;

namespace MenuMate.Extensions;

public static class ClientDAOExtensions
{
    public static Client ToClient (this ClientDAO dao, bool maskClientId = true)
    {
        return new Client
        {
            Id = (Guid)(maskClientId?Guid.Empty:((dao.Id == null || dao.Id == Guid.Empty)? Guid.Empty: dao.Id)),
            Name = dao.Name,
            Prenume = dao.Prenume
        };
    }
}