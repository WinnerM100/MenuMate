

using MenuMate.Configuration.Kafka;

namespace MenuMate.Services;

public interface IConfigService
{
    public KafkaProducerConfig GetKafkaProducerConfig();
}