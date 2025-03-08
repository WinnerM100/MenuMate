

namespace MenuMate.Models;

public class Role
{
    public Guid Id {get; set;}

    public string Name {get; set;}

    public string Value {get; set;}

    public List<User> Users{get; private set;} = new List<User>();
}