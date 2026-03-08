using System.Text.Json.Serialization;
using Confluent.Kafka;
using Newtonsoft.Json;
using OrderServiceApi.Api.Data.Dtos.Requests;
using OrderServiceApi.Api.Service.Interface;

namespace OrderServiceApi.Api.Service.Implementation
{
    public class KafkaEventPublisher : IEventPublisher, IDisposable
    {
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<KafkaEventPublisher> _logger;
        private string topic = Environment.GetEnvironmentVariable("KafkaTopicName")!;
        private string bootstrapServers = Environment.GetEnvironmentVariable("KafkaBootstrapServers")!;
        private string saslUsername = Environment.GetEnvironmentVariable("SaslUsername")!;
        private string saslPassword = Environment.GetEnvironmentVariable("SaslPassword")!;

        public KafkaEventPublisher(ILogger<KafkaEventPublisher> logger)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism    = SaslMechanism.Plain,
                SaslUsername     = saslUsername,
                SaslPassword     = saslPassword
            };
            _producer = new ProducerBuilder<string, string>(producerConfig).Build();
            _logger = logger;
        }

        public async Task PublishAsync (NotificationEvent evt, CancellationToken ct = default)
        {
            try
            {
                var message = new Message<string, string>
                {
                    Key = evt.Module,
                    Value = JsonConvert.SerializeObject(evt)
                };

                var result = await _producer.ProduceAsync(topic, message, ct);
                _logger.LogInformation($"Event published to Kafka topic {topic} at offset {result.Offset} with key {message.Key} and value {message.Value}");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error publishing event to Kafka ==> {ex.Message}");
            }
        }
        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}