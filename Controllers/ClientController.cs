using MenuMate.Models;
using MenuMate.Utilities.Sql;
using Microsoft.AspNetCore.Mvc;

namespace MenuMate.Controllers;

[ApiController]
[Route("/clients")]
public class ClientController : ControllerBase
{
    SqlConnector _conn {get; set;}

    public ClientController(SqlConnector newConn)
    {
        _conn = newConn;
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
}