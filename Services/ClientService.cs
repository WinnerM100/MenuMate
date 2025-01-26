using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq.Expressions;
using MenuMate.Caching;
using MenuMate.Constants.Exceptions;
using MenuMate.Context;
using MenuMate.DAOs;
using MenuMate.DTOs;
using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Utilities;
using MenuMate.Utilities.Sql;
using Microsoft.EntityFrameworkCore;

namespace MenuMate.Services;

class ClientService : IClientService
{
    SqlConnector connector;
    ClientContext clientContext;

    RolesSettings rolesSettings;
    RoleCache roleCache;
    private readonly IUserService userService;
    private readonly IConfiguration configuration;

    public ClientService(SqlConnector newConnector, ClientContext newClientContext, RolesSettings rolesSettings, RoleCache roleCache, IUserService userService, IConfiguration configuration)
    {
        connector = newConnector;
        clientContext = newClientContext;
        this.rolesSettings = rolesSettings;
        this.roleCache = roleCache;
        this.userService = userService;
        this.configuration = configuration;
    }

    public ClientDTO AddClientTest(ClientDTO newClient)
    {
        Client client = newClient.AsClient();

        string insertCmd = $"Insert into Client (Id, Email, Password, Nume, Prenume) VALUES ('{client.Id}', '{client.Email}', '{client.Password}', '{client.Nume}', '{client.Prenume}')";

        SqlCommand createCmd = new SqlCommand(insertCmd, connector.DbConnection);
        
        try
        {
            if(connector.DbConnection.State != System.Data.ConnectionState.Open)
            {
                connector.DbConnection.Open();
            }

            createCmd.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error Message:${ex.Message}.Complete Stack Trace: ${ex.StackTrace}");
        }
        finally
        {
            connector.DbConnection.Close();
        }

        return client.AsClientDTO(true);
    }
    public ClientDTO AddClient(ClientDAO newClient)
    {
        Role role = roleCache.GetRole(rolesSettings.GetRoleByKey("CLIENT").Name);

        string hashedPassword = SaltHash.ComputeHash(newClient.Password, configuration.GetSection("Security").GetValue<string>("HashAlgorithm"));

        User createdUser = new User()
        {
            Email = newClient.Email,
            Password = hashedPassword
        };


        //userService.CreateUser(createdUser.AsUserDTO(), role);
        
        createdUser.Roles.Add(role);

        Client addedClient = new Client(newClient with
                            {
                                Password = hashedPassword
                            }, createdUser);
                            
        clientContext.clients.Add(addedClient);
        clientContext.SaveChanges();

        return addedClient.AsClientDTO(true);
    }
    private bool CompareClientAgainstRequest(Client client, ClientDAO request)
    {
        if (client == null || request == null)
            return false;
        if(client.Id != request.Id)
            return false;
        if(!string.IsNullOrEmpty(request.Email) && !request.Email.Equals(client.Email))
            return false;
        if(!string.IsNullOrEmpty(request.Password) && !request.Password.Equals(client.Password))
            return false;
        if(!string.IsNullOrEmpty(request.Nume) && !request.Nume.Equals(client.Nume))
            return false;
        if(!string.IsNullOrEmpty(request.Prenume) && !request.Prenume.Equals(client.Prenume))
            return false;
        return true;
    }
    public ClientDTO UpdateClient(ClientDAO clientToUpdate)
    {
        Client? targetClient;
        targetClient = clientContext.clients.SingleOrDefault(client => CompareClientAgainstRequest(client, clientToUpdate)) ;

        if(null == targetClient)
        {
            throw new NotFoundException("client", clientToUpdate.ToString());
        }

        targetClient = targetClient with
        {
            Email = !string.IsNullOrWhiteSpace(clientToUpdate.Email)? clientToUpdate.Email : targetClient.Email,
            Password = !string.IsNullOrWhiteSpace(clientToUpdate.Password)? clientToUpdate.Password : targetClient.Password,
            Nume = !string.IsNullOrWhiteSpace(clientToUpdate.Nume)? clientToUpdate.Nume : targetClient.Nume,
            Prenume = !string.IsNullOrWhiteSpace(clientToUpdate.Prenume)? clientToUpdate.Prenume : targetClient.Prenume,
        };

        clientContext.clients.Update(targetClient);

        clientContext.SaveChanges();

        return targetClient.AsClientDTO();
    }

    public ClientDTO GetClientById(Guid Id, bool encapsulateIdInResponse = false)
    {
        if(Id == Guid.Empty)
        {
            throw new InvalidInputException("Id");
        }

        Client? target = clientContext.clients.AsNoTracking().FirstOrDefault(client => client.Id == Id);

        if(target == null)
        {
            throw new NotFoundException("client", $"Id = '{Id}'");
        }

        return target.AsClientDTO(encapsulateIdInResponse);
    }

    public IEnumerable<ClientDTO> GetClientByNumePrenume(string nume, string prenume, bool encapsulateId = false)
    {
        if(String.IsNullOrWhiteSpace(nume) && String.IsNullOrWhiteSpace(prenume))
        {
            throw new InvalidInputException("nume, prenume");
        }

        IEnumerable<Client> foundClients = from client in clientContext.clients.AsNoTracking()
                                           where (client.Nume+client.Prenume).Equals(nume+prenume)
                                           select client;

        if(foundClients == null || foundClients.Count() <= 0)
        {
            throw new NotFoundException("client", $"nume = '{nume}', prenume = '{prenume}'");
        }

        return foundClients.Select(client => client.AsClientDTO(encapsulateId));
    }

    public ClientDTO DeleteClientById(Guid Id)
    {
        Client? target = GetClientById(Id, true).AsClient();

        //clientContext.clients.Remove(target);
        clientContext.clients.Entry(target).State = EntityState.Deleted;
        clientContext.Remove(target);
        clientContext.SaveChanges();

        return target.AsClientDTO();
    }

    public IEnumerable<ClientDTO> DeleteClientByNumePrenume(string nume, string prenume)
    {
        IEnumerable<ClientDTO> targetClients = GetClientByNumePrenume(nume, prenume, true);

        foreach(ClientDTO client in targetClients)
        {
            if(client.Id != Guid.Empty)
            {
                ClientDTO target = DeleteClientById(client.Id);
            }
        }
        clientContext.SaveChanges();
        return targetClients;
    }

    public IEnumerable<ClientDTO> GetAllClients(bool sendId)
    {
        var clients = clientContext.clients.AsNoTracking().ToList();
        return clients.Select(c => c.AsClientDTO(sendId));
    }
}