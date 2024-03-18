using MenuMate.DTOs;

namespace MenuMate.Services;

public interface IClientService
{
    public ClientDTO CreateNewClient(ClientDTO newClient);
}