using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

public interface IRezervareService
{
    public RezervareDAO? CreateRezervareForClient(ClientDTO clientDTO);
}