

using System.ComponentModel.DataAnnotations.Schema;
using MenuMate.Models;

public class Rezervare
{
    public Guid Id { get; init; }

    public Guid ClientId { get; init; }

    public string NrTelefon { get; set; }

    public string NrPersoane { get; set; }

    public DateTime Data { get; set; }

    public string Comentarii { get; set; }

    [ForeignKey("ClientId")]
    public Client client { get; set; }
}