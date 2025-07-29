
namespace MenuMate.Configuration.HttpClientConfig;

public class HttpClientSettings
{
    public string ServiceName { get; set; }
    public string BaseURL { get; set; }

    public HttpOptions HttpOptions { get; set; }

    public HttpClientSettings(IConfigurationSection configurationSection)
    {
        ServiceName = configurationSection.Key;

        IConfigurationSection serviceSection = configurationSection.GetSection("ServiceName");
        BaseURL = serviceSection.GetSection("BaseURL").Value ?? "";
        
        HttpOptions = serviceSection.GetSection("HttpOptions")?.Get<HttpOptions>() ?? new HttpOptions("5m", 0);
    }
}