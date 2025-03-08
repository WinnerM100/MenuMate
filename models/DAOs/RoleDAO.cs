
using MenuMate.Models.DTOs;

namespace MenuMate.Models.DAOs;

public class RoleDAO
{
    public Guid? Id {get; set;}

    public string Name {get; set;}

    public string Value {get; set;}

     public override string ToString()
    {
        return $"Role:{{Id:'{Id}', Name: '{Name}', Value: '{Value}'}}";
    }
}