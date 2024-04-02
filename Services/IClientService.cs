using MenuMate.DAOs;
using MenuMate.DTOs;

namespace MenuMate.Services;

public interface IClientService
{
    public ClientDTO AddClient(ClientDAO newClient);
    public ClientDTO UpdateClient(ClientDAO newClient);
    public ClientDTO GetClientById(Guid Id, bool encapsulateIdInResponse = false);
    public IEnumerable<ClientDTO> GetClientByNumePrenume(string nume, string prenume, bool encapsulateId = false);
    public ClientDTO DeleteClientById(Guid Id);
    public IEnumerable<ClientDTO> DeleteClientByNumePrenume(string nume, string prenume);
    public IEnumerable<ClientDTO> GetAllClients(bool sendId);
}