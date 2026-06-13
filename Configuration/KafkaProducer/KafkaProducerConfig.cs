
using MenuMate.Constants.Enums;
using MenuMate.Constants.Exceptions;

namespace MenuMate.Configuration.Kafka;

public class KafkaProducerConfig
{
    public List<string> BootstrapServers;
    public string ClientId;

    public KafkaSecurityProtocol SecurityProtocol;

    public string Acks;

    public int MessageTimeoutMs;

    public int BatchNumMessages;

    public KafkaCompressionType CompressionType;

    public KafkaProducerConfig(IConfiguration configuration)
    {
        IConfigurationSection kafkaSection = configuration.GetSection(KafkaConstants.KAFKA_CONFIGURATION_KEY);

        if(kafkaSection == null)
        {
            throw new NotConfiguredException(KafkaConstants.KAFKA_CONFIGURATION_KEY);
        }

        string? bootstrapServerCSV = GetFieldValueFromKafkaConfig<string>(kafkaSection, nameof(BootstrapServers), String.IsNullOrWhiteSpace);

        this.BootstrapServers = bootstrapServerCSV.Split(',').ToList();

        this.ClientId = GetFieldValueFromKafkaConfig<string>(kafkaSection, nameof(ClientId), String.IsNullOrWhiteSpace);

        this.SecurityProtocol = GetFieldValueFromKafkaConfig<KafkaSecurityProtocol>(kafkaSection, nameof(SecurityProtocol));

        this.Acks = GetFieldValueFromKafkaConfig<string>(kafkaSection, nameof(Acks), String.IsNullOrWhiteSpace);

        this.MessageTimeoutMs = GetFieldValueFromKafkaConfig<int>(kafkaSection, nameof(MessageTimeoutMs));

        this.BatchNumMessages = GetFieldValueFromKafkaConfig<int>(kafkaSection, nameof(BatchNumMessages));

        this.CompressionType = GetFieldValueFromKafkaConfig<KafkaCompressionType>(kafkaSection, nameof(CompressionType));
    }
    
    private FieldType GetFieldValueFromKafkaConfig<FieldType>(IConfiguration kafkaConfigSection, string fieldName, Func<FieldType?, bool>? isEmptyCheck = null)
    {
        FieldType fieldValue = kafkaConfigSection.GetValue<FieldType>(fieldName);

        if(isEmptyCheck == null)
        {
            if(fieldValue == null)
            {
                throw new NotConfiguredException(fieldName);
            }
        }
        else
        {
            if(isEmptyCheck(fieldValue))
            {
                throw new NotConfiguredException(fieldName);
            }
        }

        return fieldValue;
    }

}