using AcademyProjectCaches.CacheInMemoryCollection;
using AcademyProjectModels.CongigurationSettings;
using AcademyProjectModels.KafkaModels;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AcademyProjectCaches
{
    public class KafkaGenericConsumer<TKey, TValue> : IHostedService
    {
        private readonly ILogger<KafkaGenericConsumer<TKey, TValue>> _logger;
        private readonly IOptionsMonitor<KafkaSubscriberSettings> _subSettings;
        private ConsumerConfig _consumerConfig;
        private IConsumer<TKey, TValue> _consumer;
        private GenericCollection<TValue> _genericCollection;

        public KafkaGenericConsumer(ILogger<KafkaGenericConsumer<TKey, TValue>> logger, IOptionsMonitor<KafkaSubscriberSettings> subSettings, GenericCollection<TValue> genericCollection)
        {
            _logger = logger;
            _subSettings = subSettings;
            _genericCollection = genericCollection;
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _subSettings.CurrentValue.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset)_subSettings.CurrentValue.AutoOffsetReset,
                GroupId = _subSettings.CurrentValue.GroupId,
            };
            _consumer = new ConsumerBuilder<TKey, TValue>(_consumerConfig).SetKeyDeserializer(new MessagePakcDeserializer<TKey>())
                .SetValueDeserializer(new MessagePakcDeserializer<TValue>()).Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_subSettings.CurrentValue.TopicName);
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumer.Consume();
                    _genericCollection.Add(cr.Message.Value);
                    _logger.LogInformation($"Added {cr.Message.Key}, {cr.Message.Value}");
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}