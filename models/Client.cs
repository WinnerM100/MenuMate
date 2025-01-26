
namespace MenuMate.Models
{
    public record Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Prenume { get; set; }

        public override string ToString()
        {
            return $"Client[Id:'{Id}', Name: '{Name}', Prenume: '{Prenume}']";
        }
    }
}