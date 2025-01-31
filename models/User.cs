using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuMate.Models;

public record User
{   
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }
    
    public ICollection<Role> Roles{ get; private set; } = new List<Role>();

    public User()
    {
        Id = Guid.NewGuid();
    }
    public User(IEnumerable<Role> roles)
    {
        Id = Guid.NewGuid();
        Roles = roles.ToList();
    }
}