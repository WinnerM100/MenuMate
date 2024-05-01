using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuMate.Models;

public class UsersRoles
{
    [Key, Column(Order = 0)]
    public Guid RoleId { get; set; }

    [Key, Column(Order = 1)]
    public Guid UserId { get; set; }
    public User User{ get; set; }
    public Role Role { get; set; }
}