

using MenuMate.AccessLayer.Context;
using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
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

    public ClientDAO? DeleteClient(ClientDTO clientDetails)
    {
        Client toBeDeletedClient = GetClientByDetails(clientDetails)?.ToClient(false) ?? null;

        if (null == toBeDeletedClient)
        {
            return null;
        }

        dbContext.Clients.Remove(toBeDeletedClient);
        dbContext.SaveChangesAsync();

        return toBeDeletedClient.ToClientDAO();
    }

    public IEnumerable<ClientDAO> GetAllClients()
    {
        return dbContext.Clients.Select(c => c.ToClientDAO(true)).ToList();
    }

    public ClientDAO GetClientById(Guid clientID)
    {
        Client targetClient = dbContext.Clients.FirstOrDefault(c => c.Id == clientID);
        
        return targetClient?.ToClientDAO(false) ?? null;
    }

    public ClientDAO? GetClientByDetails(ClientDTO clientDetails)
    {
        Client targetClient = dbContext.Clients.FirstOrDefault(c =>
            (clientDetails.Id == null || clientDetails.Id.Equals(Guid.Empty) || clientDetails.Id == c.Id) &&
            clientDetails.Name.ToLower().Equals(c.Name.ToLower()) &&
            clientDetails.Prenume.ToLower().Equals(c.Prenume.ToLower()));
        
        return targetClient?.ToClientDAO(false) ?? null;
    }

    public ClientDAO GetClientByNameAndPrenume(string name, string prenume)
    {
        throw new NotImplementedException();
    }

    public ClientDAO? UpdateClient(ClientDTO clientDetails)
    {
        Client clientToBeUpdated = GetClientById(clientDetails.Id ?? Guid.Empty)?.ToClient(false) ?? null;

        if(clientToBeUpdated == null)
        {
            return null;
        }

        dbContext.Clients.Update(new Client()
        {
            Id = clientToBeUpdated.Id,
            Name = clientDetails.Name,
            Prenume = clientDetails.Prenume
        });

        dbContext.SaveChangesAsync();

        return clientToBeUpdated.ToClientDAO();
    }
}