
using Microsoft.Extensions.Configuration;

namespace MenuMate.Configuration.HttpClientConfig;

public class HttpClientSettings
{
    public string ServiceName { get; set; }
    public string BaseURL { get; set; }

    public HttpOptions HttpOptions { get; set; }

    public HttpClientSettings(IConfigurationSection configurationSection)
    {
        ServiceName = configurationSection.Key;

        BaseURL = configurationSection.GetSection("BaseURL").Value ?? "";
        
        HttpOptions = configurationSection.GetSection("HttpOptions")?.Get<HttpOptions>() ?? new HttpOptions("5m", 0);
    }
}