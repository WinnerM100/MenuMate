

using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public interface IAuthService
{
    public string GetTokenForUser(UserDTO userDTO);
}