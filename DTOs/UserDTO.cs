using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuMate.DTOs;

public class UserDTO
{   
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UserDTO()
    {
        Id = Guid.NewGuid();
    }
}