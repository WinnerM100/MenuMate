using System.Data.SqlClient;
using MenuMate.DAOs;
using MenuMate.DTOs;
using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Services;
using MenuMate.Utilities.Sql;
using Microsoft.AspNetCore.Mvc;

namespace MenuMate.Controllers;

[ApiController]
[Route("/clients")]
public class ClientController : ControllerBase
{
    SqlConnector _conn;
    IClientService clientService;

    public ClientController(SqlConnector newConn, IClientService newClientService)
    {
        _conn = newConn;
        clientService = newClientService;
    }

    [HttpPost]
    public ActionResult<ClientDTO> CreateNewClient([FromBody]ClientDAO newClient)
    {
        var result = clientService.AddClient(newClient);

        return result;
    }

    [HttpPut]
    public ActionResult<ClientDTO> UpdateClient([FromBody]ClientDAO clientToUpdate)
    {
        var result = clientService.UpdateClient(clientToUpdate);

        return result;
    }

    [HttpGet]
    [Route("/{id}")]
    public ActionResult<ClientDTO> GetClientById(Guid id)
    {
        var result = clientService.GetClientById(id);

        return result;
    }

    [HttpGet]
    [Route("/search")]
    public ActionResult<IEnumerable<ClientDTO>> GetClientByNumePrenume([FromQuery(Name = "nume")]string nume="", [FromQuery(Name = "prenume")]string prenume="")
    {
        try
        {
            var result = clientService.GetClientByNumePrenume(nume, prenume);

            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest($"Exception executing method {nameof(GetClientByNumePrenume)}: {ex.Message}\n{ex.InnerException}");
        }
    }

    [HttpDelete]
    [Route("/{id}")]
    public ActionResult<ClientDTO> DeleteClientById(Guid id)
    {
        var result = clientService.DeleteClientById(id);

        return result;
    }

    [HttpDelete]
    public ActionResult<IEnumerable<ClientDTO>> DeleteClientByNumePrenume([FromQuery(Name = "nume")]string nume="", [FromQuery(Name = "prenume")]string prenume="")
    {
        try
        {
            var result = clientService.DeleteClientByNumePrenume(nume, prenume);

            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest($"Exception executing method {nameof(GetClientByNumePrenume)}: {ex.Message}\n{ex.InnerException}");
        }
    }

    [HttpGet]
    [Route("/all")]
    public ActionResult<IEnumerable<ClientDTO>> GetAllClients()
    {
        return Ok(clientService.GetAllClients(true));
    }
}