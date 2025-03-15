using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;
using MenuMate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class RezervareController : ControllerBase
{
    private IRezervareService rezervareService;

    public RezervareController(IRezervareService rezervareService)
    {
        this.rezervareService = rezervareService;
    }

    // public ActionResult<RezervareDAO> CreateRezervareForClient(ClientDTO clientDTO)
    // {
    //     RezervareDAO? newlyCreatedRezervare = rezervareService.CreateRezervareForClient(clientDTO);

    //     return Ok(newlyCreatedRezervare);
    // }
}