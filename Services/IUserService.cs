
using MenuMate.DTOs;
using MenuMate.Models;

namespace MenuMate.Services;

public interface IUserService
{
    public User GetUser(UserDAO loginUser);

    public User GetUserByEmail(string email);

    public string LoginUser(UserDTO userDTO);
    public UserDAO CreateUser(UserDTO userDTO, Role role);
}