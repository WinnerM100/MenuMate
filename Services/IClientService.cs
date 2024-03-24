using MenuMate.DTOs;

namespace MenuMate.Services;

public interface IClientService
{
    public ClientDTO AddClient(ClientDTO newClient);
    public ClientDTO UpdateClient(ClientDTO newClient);
    public ClientDTO GetClientById(Guid Id);
    public IEnumerable<ClientDTO> GetClientByNumePrenume(string nume, string prenume);
    public ClientDTO DeleteClientById(Guid Id);
    public ClientDTO DeleteClientByNumePrenume(string nume, string prenume);
    public IEnumerable<ClientDTO> GetAllClients();
}