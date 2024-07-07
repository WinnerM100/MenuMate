using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MenuMate.Models;

public class UsersRoles
{
    [Key]
    public Guid Id { get; private set; }
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }
    public User User{ get; set; }
    public Role Role { get; set; }

    public UsersRoles()
    {
        Id = new Guid();
    }
}