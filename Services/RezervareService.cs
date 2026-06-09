 using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MenuMate.AccessLayer.Context;
using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public class RezervareService : IRezervareService
{
    private MenuMateContext dbContext { get; set; }

    private readonly IHttpClientFactory httpClientFactory;

    private readonly IClientService clientService;

    public RezervareService(MenuMateContext dbContext, IHttpClientFactory httpClientFactory, IClientService clientService)
    {
        this.dbContext = dbContext;
        this.httpClientFactory = httpClientFactory;
        this.clientService = clientService;
    } 
    public async Task<RezervareResponse?> CreateRezervareForClient(RezervareDetails rezervareDetails)
    {
        Client? targetClient = clientService.GetClientById(rezervareDetails.ClientId);

        if (targetClient == null)
        {
            return new RezervareResponse
            {
                rezervareOptions = RezervareOptions.ClientNotFound
            };
        }
        ConfirmareRezervare? confirmareRezervare;
        using (HttpClient rezervariAPIClient = httpClientFactory.CreateClient("RezervariAPI"))
        {

            var httpClientResponse = await rezervariAPIClient.PostAsJsonAsync("Rezervare", rezervareDetails);
            
            if(!httpClientResponse.IsSuccessStatusCode)
            {
                string jsonRezervariApiFailedResponse = await httpClientResponse.Content.ReadAsStringAsync();
                return new RezervareResponse
                {
                    rezervareOptions = RezervareOptions.RezervareFailed,
                    confirmareRezervareDetails = jsonRezervariApiFailedResponse
                };
            }

            string jsonRezervariSuccessApiResponse = await httpClientResponse.Content.ReadAsStringAsync();
            confirmareRezervare = await httpClientResponse.Content.ReadFromJsonAsync<ConfirmareRezervare>();

            if(confirmareRezervare is null)
            {
                return new RezervareResponse
                {
                    rezervareOptions = RezervareOptions.RezervareFailed,
                    confirmareRezervareDetails = jsonRezervariSuccessApiResponse
                };

            }
            return new RezervareResponse
            {
                rezervareOptions = RezervareOptions.RezervareSucces,
                confirmareRezervareDetails = confirmareRezervare
            };
        }
    }
}
