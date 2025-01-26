
namespace MenuMate.Models.DAOs
{
    public record ClientDAO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Prenume { get; set; }

        public override string ToString()
        {
            return $"ClientDTO[Id:'{Id}', Name: '{Name}', Prenume: '{Prenume}']";
        }
    }
}