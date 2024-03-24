using System.Data.SqlClient;
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

    [HttpGet]
    public ActionResult<string> Test()
    {
        Client newClient = new Client{
            Email = "cascascascas",
            Password = "cascascascasc",
            Nume = "Croitorescu",
            Prenume = "Madalin"
        };


        return $"Hello, World!${newClient}";
    }

    [HttpPost]
    public ActionResult<ClientDTO> CreateNewClient([FromBody]ClientDTO newClient)
    {
        var result = clientService.AddClient(newClient);

        return result;
    }

    [HttpPut]
    public ActionResult<ClientDTO> UpdateClient([FromBody]ClientDTO clientToUpdate)
    {
        var result = clientService.UpdateClient(clientToUpdate);

        return result;
    }
}