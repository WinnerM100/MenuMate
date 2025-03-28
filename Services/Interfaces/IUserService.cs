

using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public interface IUserService
{
    public User? CreateUserForClient(Guid clientId, UserDTO userDTO);

    public User? GetUserByEmailAndPassword(string email, string password);

    public UserDAO? UpdateUserById (UserDTO userDTO, Guid Id);

    public UserDAO? DeleteUserByEmailAndPassword(string email,string password);
}