using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuMate.Models;

public class User
{   
    [Key]
    public Guid Id { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }

    public ICollection<Role> Roles{ get; set; }

    [InverseProperty("User")]
    public Client client{ get; set; }
}