

using MenuMate.AccessLayer.Context;
using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;
using MenuMate.Services;
using Microsoft.AspNetCore.Mvc;

namespace MenuMate.Controllers;

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
    public ActionResult<IEnumerable<ClientDAO>> GetClients()
    {   
        return Ok(clientService.GetAllClients());
    }

    [HttpPost]
    public ActionResult<ClientDAO> CreateClient(ClientDTO clientDetails)
    {
        ClientDAO clientDAO = clientService.CreateClient(clientDetails);

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

    [HttpPut]
    [Route("/{id}")]
    public ActionResult<ClientDAO> UpdateClient(ClientDTO clientDTO)
    {
        ClientDAO? updatedClient = clientService.UpdateClient(clientDTO) ?? null;

        if(updatedClient == null)
        {
            return NotFound($"No client found using the following data: {clientDTO}");
        }

        return Ok(updatedClient);
    }
}