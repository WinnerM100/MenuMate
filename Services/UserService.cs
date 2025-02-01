

using MenuMate.AccessLayer.Context;
using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public class UserService : IUserService
{

    private MenuMateContext dbContext { get; set; }

    public UserService(MenuMateContext context)
    {
        this.dbContext = context;
    }

    public User? CreateUserForClient(Guid clientId, UserDTO userDTO)
    {
        Client? targetClient = dbContext.Clients.FirstOrDefault(c => c.Id == clientId);

        if (targetClient == null)
        {
            return null;
        }
        User newUser = (userDTO with
        {
            Id = Guid.NewGuid()
        }).ToUser();
        targetClient.User = newUser;

        dbContext.Users.Add(newUser);

        dbContext.SaveChangesAsync();

        return newUser;
    }
}