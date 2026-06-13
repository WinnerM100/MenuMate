using MenuMate.Configuration.Kafka;

namespace MenuMate.Services;

public class ConfigService : IConfigService
{
    private IConfiguration configuration;

    public ConfigService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public KafkaProducerConfig GetKafkaProducerConfig()
    {
        return new KafkaProducerConfig(configuration);
    }


}