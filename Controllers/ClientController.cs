

using MenuMate.AccessLayer.Context;
using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;
using MenuMate.Security.Authorization.Attributes;
using MenuMate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuMate.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private IClientService clientService { get; set;}

    public ClientController(IClientService clientService)
    {
        this.clientService = clientService;
    }

    [HttpGet]
    [HasRole]
    public ActionResult<IEnumerable<ClientDTO>> GetClients()
    {   
        List<Client> allClients = clientService.GetAllClients().ToList();

        if(allClients == null || allClients.Count == 0)
        {
            return NoContent();
        }
    
        return Ok(allClients.Select(c => c.ToClientDAO()));
    }

    [HttpPost]
    public ActionResult<ClientDAO> CreateClient(ClientDTO clientDetails)
    {
        ClientDAO? clientDAO = clientService.CreateClient(clientDetails);

        if (null == clientDAO)
        {
            return BadRequest($"Unable to create client with the following details: {clientDetails}");
        }
        else return Ok(clientDAO);
    }

    [HttpDelete]
    public ActionResult<ClientDAO> DeleteClient(ClientDTO clientDTO)
    {
        ClientDAO? toBeDeletedClient = clientService.DeleteClient(clientDTO);

        if(toBeDeletedClient == null)
        {
            return NotFound($"No client found using the following data: {clientDTO}");
        }

        return Ok(toBeDeletedClient);
    }

    [HttpPut("{customerId}")]
    public ActionResult<ClientDAO> UpdateClient(Guid customerId, [FromBody] ClientDTO clientDTO)
    {
        Client? updatedClient = clientService.UpdateClient(clientDTO with {
            Id = customerId
        }) ?? null;

        if(updatedClient == null)
        {
            return NotFound($"No client found using the following data: {clientDTO}");
        }

        return Ok(updatedClient.ToClientDAO());
    }
}