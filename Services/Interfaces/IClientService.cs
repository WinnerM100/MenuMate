
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public interface IClientService
{
    public ClientDAO GetClientById(Guid clientID);

    public ClientDAO GetClientByNameAndPrenume(string name, string prenume);

    public IEnumerable<ClientDAO> GetAllClients();

    public ClientDAO CreateClient(ClientDTO clientDetails);

    public ClientDAO? UpdateClient(ClientDTO clientDetails);

    public ClientDAO? DeleteClient(ClientDTO clientDetails);
}