using System.Data.SqlClient;
using MenuMate.DTOs;
using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Utilities.Sql;

namespace MenuMate.Services;

class ClientService : IClientService
{
    SqlConnector connector;

    public ClientService(SqlConnector newConnector)
    {
        connector = newConnector;
    }

    public ClientDTO CreateNewClient(ClientDTO newClient)
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
}