namespace MenuMate.Configuration.HttpClientConfig;

public class HttpOptions
{
    public string TimeSpan { get; set; }

    public int Retries { get; set; }

    public HttpOptions(string timeSpan, int retries)
    {
        TimeSpan = timeSpan;
        Retries = retries;
    }
    
}