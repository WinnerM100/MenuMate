using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using MenuMate.Constants.Exceptions;
using MenuMate.Context;
using MenuMate.DTOs;
using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Utilities.Sql;

namespace MenuMate.Services;

class ClientService : IClientService
{
    SqlConnector connector;
    ClientContext clientContext;

    public ClientService(SqlConnector newConnector, ClientContext newClientContext)
    {
        connector = newConnector;
        clientContext = newClientContext;
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
    public ClientDTO AddClient(ClientDTO newClient)
    {
        Client client = clientContext.clients.Add(newClient.AsClient());
        
        clientContext.SaveChanges();

        return client.AsClientDTO();
    }

    public ClientDTO UpdateClient(ClientDTO clientToUpdate)
    {
        if(clientToUpdate.Id == null || clientToUpdate.Id == Guid.Empty)
        {
            throw new InvalidClientDataException("Id");
        }

        Client? targetClient = clientContext.clients.SingleOrDefault(client => client.Id == clientToUpdate.Id);

        if(null == targetClient)
        {
            throw new Exception("No Client was found with input data.");
        }

        targetClient = targetClient with
        {
            Email = !string.IsNullOrWhiteSpace(clientToUpdate.Email)? clientToUpdate.Email : targetClient.Email,
            Password = !string.IsNullOrWhiteSpace(clientToUpdate.Password)? clientToUpdate.Password : targetClient.Password,
            Nume = !string.IsNullOrWhiteSpace(clientToUpdate.Nume)? clientToUpdate.Nume : targetClient.Nume,
            Prenume = !string.IsNullOrWhiteSpace(clientToUpdate.Prenume)? clientToUpdate.Prenume : targetClient.Prenume,
        };

        clientContext.clients.AddOrUpdate<Client>(targetClient);

        clientContext.SaveChanges();

        return targetClient.AsClientDTO();
    }

    public ClientDTO GetClientById(Guid Id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ClientDTO> GetClientByNumePrenume(string nume, string prenume)
    {
        throw new NotImplementedException();
    }

    public ClientDTO DeleteClientById(Guid Id)
    {
        throw new NotImplementedException();
    }

    public ClientDTO DeleteClientByNumePrenume(string nume, string prenume)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ClientDTO> GetAllClients()
    {
        throw new NotImplementedException();
    }
}