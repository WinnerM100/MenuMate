using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuMate.Models;

public class Role
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }

    public ICollection<UsersRoles> UsersRoles{ get; set; }

    public Role()
    {
        Id = Guid.NewGuid();
    }
}