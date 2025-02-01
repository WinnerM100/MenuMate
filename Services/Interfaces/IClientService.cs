
using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public interface IClientService
{
    public Client GetClientById(Guid clientID);

    public ClientDAO GetClientByNameAndPrenume(string name, string prenume);

    public IEnumerable<Client> GetAllClients();

    public ClientDAO CreateClient(ClientDTO clientDetails);

    public Client? UpdateClient(ClientDTO clientDetails);

    public ClientDAO? DeleteClient(ClientDTO clientDetails);
}