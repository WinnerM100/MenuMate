

namespace MenuMate.Models;
public class RezervareDetails
{
    public Guid Id { get; init; }

    public Guid ClientId { get; init; }

    public string NrTelefon { get; set; }

    public string NrPersoane { get; set; }

    public DateTime Data { get; set; }

    public string Comentarii { get; set; }
}