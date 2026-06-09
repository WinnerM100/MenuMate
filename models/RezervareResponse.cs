using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

public class RezervareResponse
{
    public RezervareOptions rezervareOptions = RezervareOptions.RezervareSucces;

    public object confirmareRezervareDetails;

}

public enum RezervareOptions
{
    ClientNotFound,
    RezervareFailed,
    RezervareSucces
}