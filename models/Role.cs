using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuMate.Models;

public record Role
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public ICollection<User> Users{ get; private set; } = new List<User>();

    public Role()
    {
        Id = Guid.NewGuid();
    }

    public override string ToString()
    {
        return $"{{Id:{Id}, Name:{Name}, Value:{Value}}}";
    }
}