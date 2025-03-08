
using System.Text;
using MenuMate.Models.DAOs;

namespace MenuMate.Models.DTOs;

public record UserDAO
{
    public Guid? Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public ICollection<RoleDAO> Roles{ get; set; } = new List<RoleDAO>();

    public override string ToString()
    {
        StringBuilder stringBuilder= new StringBuilder();
        stringBuilder.Append("UserDAO[Id='").Append(Id).Append('\'')
                     .Append(", Email='").Append(Email).Append('\'')
                     .Append(", Password='").Append(Password).Append('\'')
                     .Append("Roles:[");
        foreach(RoleDAO role in Roles)
        {
            stringBuilder.Append(role).Append(',');
        }
        return stringBuilder.ToString().Remove(stringBuilder.Length-1);
    }
}