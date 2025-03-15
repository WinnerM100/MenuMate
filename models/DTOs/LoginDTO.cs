

namespace MenuMate.Models;
public class LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        return $"Login[Email='{Email}', Password='{Password}']";
    }
}