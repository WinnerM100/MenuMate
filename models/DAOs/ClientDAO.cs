
using MenuMate.Models.DTOs;

namespace MenuMate.Models.DAOs
{
    public record ClientDAO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Prenume { get; set; }
        public UserDAO? UserDAO { get; set; }
        public override string ToString()
        {
            return $"ClientDAO[Id:'{Id}', Name: '{Name}', Prenume: '{Prenume}', UserDAO: {{{UserDAO}}}]";
        }
    }
}