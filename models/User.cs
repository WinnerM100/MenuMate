using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuMate.Models;

public class User
{   
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }

    public ICollection<UsersRoles> UsersRoles{ get; set; }

    public Guid ClientId { get; set; }
    public Client Client{ get; set; }

    public User()
    {
        Id = Guid.NewGuid();

        UsersRoles = new List<UsersRoles>();
    }
}