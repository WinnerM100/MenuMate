

using MenuMate.AccessLayer.Context;
using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;
using Microsoft.EntityFrameworkCore;

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
        targetClient.UserId = newUser.Id;
        targetClient.User = newUser;
        dbContext.Users.Add(newUser);

        dbContext.SaveChanges();

        return newUser;
    }

    public UserDAO? GetUserByEmailAndPassword(string email, string password)
    {
        User? targetUser = dbContext.Users.FirstOrDefault(u => u.Email.ToLower().Equals(email.ToLower()) && u.Password.Equals(password));

        if(targetUser == null)
        {
            return null;
        }

        return targetUser.ToUserDAO();
    }

    public UserDAO? UpdateUserById(UserDTO userDTO, Guid Id)
    {
        User targetUser = dbContext.Users.Where(u => u.Id == Id).FirstOrDefault();

        if(targetUser == null)
        {
            return null;
        }

        targetUser.Email = userDTO.Email;
        targetUser.Password = userDTO.Password;

        dbContext.Users.Update(targetUser);
        dbContext.SaveChanges();

        return targetUser.ToUserDAO();
    }

    public UserDAO? DeleteUserByEmailAndPassword(string email, string password)
    {
        User targetUser = dbContext.Users.Where(u => u.Email.ToLower().Equals(email.ToLower()) && u.Password.Equals(password)).FirstOrDefault();

        if(targetUser == null)
        {
            return null;
        }

        dbContext.Users.Where(u => u.Email.ToLower().Equals(email.ToLower()) && u.Password.Equals(password)).ExecuteDeleteAsync();

        return targetUser.ToUserDAO();
    }
}