

using MenuMate.AccessLayer.Context;
using MenuMate.Extensions;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public class ClientService : IClientService
{   
    private MenuMateContext dbContext { get; set; }

    public ClientService(MenuMateContext context)
    {
        this.dbContext = context;
    }
    public ClientDAO CreateClient(ClientDTO clientDetails)
    {
        dbContext.Clients.Add(clientDetails.ToClient(false));

        dbContext.SaveChanges();

        return clientDetails.ToClient().ToClientDAO();
    }

    public ClientDAO DeleteClient(ClientDTO clientDetails)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ClientDAO> GetAllClients()
    {
        return dbContext.Clients.Select(c => c.ToClientDAO(true)).ToList();
    }

    public ClientDAO GetClientById(Guid clientID)
    {
        throw new NotImplementedException();
    }

    public ClientDAO GetClientByNameAndPrenume(string name, string prenume)
    {
        throw new NotImplementedException();
    }

    public ClientDAO UpdateClient(ClientDTO clientDetails)
    {
        throw new NotImplementedException();
    }
}