

using MenuMate.Models;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public interface IUserService
{
    public User? CreateUserForClient(Guid clientId, UserDTO userDTO);
}