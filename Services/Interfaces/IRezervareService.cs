using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

public interface IRezervareService
{
    public Task<RezervareResponse?> CreateRezervareForClient(RezervareDetails rezervareDetails);
}