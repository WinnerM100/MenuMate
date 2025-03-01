using System.Data.Entity;
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
    public ClientDAO? CreateClient(ClientDTO clientDetails)
    {
        if(clientDetails.UserDTO is null)
        {
            return null;
        }
        if(string.IsNullOrEmpty(clientDetails.UserDTO.Email) || string.IsNullOrEmpty(clientDetails.UserDTO.Password))
        {
            return null;
        }

        Client newClient = clientDetails.ToClient();

        newClient.Id = Guid.NewGuid();
        newClient.User.Email = clientDetails.UserDTO.Email;
        newClient.User.Password = clientDetails.UserDTO.Password;
        newClient.User.ClientId = newClient.Id;
        newClient.User.Client = newClient;
        newClient.UserId = newClient.User.Id;

        //dbContext.Users.Add(newClient.User);
        dbContext.Clients.Add(newClient);

        dbContext.SaveChanges();

        return clientDetails.ToClient().ToClientDAO();
    }

    public ClientDAO? DeleteClient(ClientDTO clientDetails)
    {
        Client toBeDeletedClient = GetClientByDetails(clientDetails) ?? null;

        if (null == toBeDeletedClient)
        {
            return null;
        }

        dbContext.Clients.Remove(toBeDeletedClient);
        dbContext.SaveChangesAsync();

        return toBeDeletedClient.ToClientDAO();
    }

    public IEnumerable<Client> GetAllClients()
    {   
        var clients = from c in dbContext.Clients
                      join u in dbContext.Users on c.Id equals u.ClientId  
                      select new {c,u};
        return clients.ToList().Select(ent => ent.c).ToList();
    }

    public Client? GetClientById(Guid clientID)
    {
        Client targetClient = dbContext.Clients.AsNoTracking().FirstOrDefault(c => c.Id == clientID);
        
        return targetClient ?? null;
    }

    public Client? GetClientByDetails(ClientDTO clientDetails)
    {
        Client targetClient = dbContext.Clients.AsNoTracking().FirstOrDefault(c =>
            (clientDetails.Id == null || clientDetails.Id.Equals(Guid.Empty) || clientDetails.Id == c.Id) &&
            clientDetails.Name.ToLower().Equals(c.Name.ToLower()) &&
            clientDetails.Prenume.ToLower().Equals(c.Prenume.ToLower()));
        
        return targetClient ?? null;
    }

    public ClientDAO GetClientByNameAndPrenume(string name, string prenume)
    {
        throw new NotImplementedException();
    }

    public Client? UpdateClient(ClientDTO clientDetails)
    {
        Client clientToBeUpdated = GetClientById(clientDetails.Id ?? Guid.Empty) ?? null;

        if(clientToBeUpdated == null)
        {
            return null;
        }

        clientToBeUpdated.Name = clientDetails.Name;
        clientToBeUpdated.Prenume = clientDetails.Prenume;

        dbContext.Clients.Update(clientToBeUpdated);

        dbContext.SaveChangesAsync();

        return clientToBeUpdated;
    }
}