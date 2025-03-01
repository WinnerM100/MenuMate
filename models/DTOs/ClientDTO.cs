
namespace MenuMate.Models.DTOs
{
    public record ClientDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Prenume { get; set; }
        public UserDTO? UserDTO { get; set; }

        public override string ToString()
        {
            return $"ClientDTO[Id:'{Id}', Name: '{Name}', Prenume: '{Prenume}', User: {{{UserDTO}}}]";
        }
    }
}