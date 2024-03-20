using MenuMate.DTOs;

namespace MenuMate.Services;

public interface IClientService
{
    public ClientDTO AddClient(ClientDTO newClient);
}