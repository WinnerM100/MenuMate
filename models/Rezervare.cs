

using System.ComponentModel.DataAnnotations.Schema;
using MenuMate.Models;

public class Rezervare
{
    public Guid Id { get; init; }
    public Guid ClientId { get; init; }
    public string RezervareNumber { get; init; }
}