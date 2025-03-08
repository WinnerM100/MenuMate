using MenuMate.AccessLayer.Context;
using MenuMate.Constants.Enums;
using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MenuMate.Services;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
public class ClientService : IClientService
{   
    private MenuMateContext dbContext { get; set; }

    private IRoleService roleService{ get; set; }

    public ClientService(MenuMateContext context, IRoleService roleService)
    {
        this.dbContext = context;
        this.roleService = roleService;
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

        Role userRole = roleService.GetRoleByName(RoleNameEnum.CLIENT.ToString());
        newClient.User.Roles.Add(userRole);

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
        var clients = dbContext.Clients.Include(c => c.User).ThenInclude(u => u.Roles).ToList();
        return clients;
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