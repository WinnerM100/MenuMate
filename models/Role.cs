namespace MenuMate.Models;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }

    public ICollection<User> Users{ get; set; }
}