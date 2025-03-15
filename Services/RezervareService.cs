using MenuMate.AccessLayer.Context;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public class RezervareService : IRezervareService
{
    private MenuMateContext dbContext { get; set; }
    public RezervareDAO? CreateRezervareForClient(ClientDTO clientDTO)
    {
        
    }
}
