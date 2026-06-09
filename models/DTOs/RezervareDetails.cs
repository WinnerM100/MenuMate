

using System.Text.Json.Serialization;

namespace MenuMate.Models;
public class RezervareDetails
{
    [JsonIgnore]
    public Guid Id { get; init; }

    public Guid ClientId { get; init; } = Guid.Empty;

    public string NrTelefon { get; set; }

    public string NrPersoane { get; set; }

    public DateTime Data { get; set; }

    public string Comentarii { get; set; }
}