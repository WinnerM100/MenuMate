using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

public interface IRezervareService
{
    public ConfirmareRezervare? CreateRezervareForClient(ClientDTO clientDTO);
}