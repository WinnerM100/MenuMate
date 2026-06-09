namespace MenuMate.Configuration.HttpClientConfig;

public class HttpOptions
{
    public string Timeout { get; set; }

    public int Retries { get; set; }

    public HttpOptions(string timeout, int retries)
    {
        Timeout = timeout;
        Retries = retries;
    }
    public static TimeSpan ConvertToTimeSpan(string Timeout)
    {
        char timeUnit = Timeout.Last();
        switch (timeUnit)
        {
            case 's':
                return TimeSpan.FromSeconds(int.Parse(Timeout.Substring(0, Timeout.Length - 1)));
            case 'm':
                return TimeSpan.FromMinutes(int.Parse(Timeout.Substring(0, Timeout.Length - 1)));
            case 'h':
                return TimeSpan.FromHours(int.Parse(Timeout.Substring(0, Timeout.Length - 1)));
            case 'd':
                return TimeSpan.FromDays(int.Parse(Timeout.Substring(0, Timeout.Length - 1)));

            default:
                return TimeSpan.Zero;
        }
    }
}