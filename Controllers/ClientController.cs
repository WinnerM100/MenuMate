using MenuMate.Models;
using Microsoft.AspNetCore.Mvc;

namespace MenuMate.Controllers;

[ApiController]
[Route("/clients")]
public class ClientController : ControllerBase
{
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