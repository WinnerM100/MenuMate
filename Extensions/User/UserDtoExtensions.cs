using MenuMate.DTOs;
using MenuMate.Models;
using Microsoft.AspNetCore.Identity;

public static class UsertDtoExtensions
{
    public static User AsUser(this UserDTO userDTO)
    {
        return new User() with
        {
            Email = userDTO.Email,
            Password = userDTO.Password
        };
    }
}