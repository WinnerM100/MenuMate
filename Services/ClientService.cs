using System.Data.SqlClient;
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
}