

using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public interface IUserService
{
    public User? CreateUserForClient(Guid clientId, UserDTO userDTO);

    public UserDAO GetUserByEmailAndPassword(string email, string password);
}