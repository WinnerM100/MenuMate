using System.Security.Claims;
using System.Threading.Tasks;
using MenuMate.Models;
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

    [HttpPost]
    public async Task<ActionResult<ConfirmareRezervare>> CreateRezervareForClient(RezervareRequest rezervareDetails)
    {
        var userIdentity = HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

        if (userIdentity is null)
        {
           //"Calling endpoint has not been authenticated! No anonymous requests allowed for this endpoint."
           return StatusCode(401);
        }
        Claim? userDataClaim = userIdentity.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.UserData));
        if (userDataClaim is null)
        {
            //"User has no data to be processed! No claims found."
            return StatusCode(403);
        }

        RezervareDetails rezervareRequest = new RezervareDetails
        {
            ClientId = Guid.Parse(userDataClaim.Value),
            Data = rezervareDetails.Data,
            NrPersoane = rezervareDetails.NrPersoane,
            NrTelefon = rezervareDetails.NrTelefon,
            Comentarii = rezervareDetails.Comentarii
        };
        
        var confirmareRezervare = await rezervareService.CreateRezervareForClient(rezervareRequest);

        switch(confirmareRezervare.rezervareOptions)
        {
            case RezervareOptions.RezervareSucces:
                return Ok(confirmareRezervare.confirmareRezervareDetails as ConfirmareRezervare);
            case RezervareOptions.ClientNotFound:
                return BadRequest($"Clientul nu are cont facut! Extra info: {(string)confirmareRezervare.confirmareRezervareDetails}");
            case RezervareOptions.RezervareFailed:
            default:
                return BadRequest($"Ceva nu a mers cum trebuie....Rezervarea a esuat. Extra info:{(string)confirmareRezervare.confirmareRezervareDetails}");
        }
    }
}