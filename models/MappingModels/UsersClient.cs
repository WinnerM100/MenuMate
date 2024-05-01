using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuMate.Models;

public class UserClient
{
    [Key, Column(Order = 0)]
    public Guid ClientId { get; set; }

    [Key, Column(Order = 1)]
    public Guid UserId { get; set; }
    public User User{ get; set; }
    public Client Client { get; set; }
}