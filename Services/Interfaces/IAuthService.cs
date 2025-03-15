

using MenuMate.Models;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public interface IAuthService
{
    public string GetTokenForUser(UserDTO userDTO);

    public string Login (LoginDTO loginDetails);
}