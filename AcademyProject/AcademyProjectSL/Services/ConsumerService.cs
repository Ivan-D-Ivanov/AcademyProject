using System.Threading;
using AcademyProjectModels.ConfigurationSettings;
using AcademyProjectModels.CongigurationSettings;
using AcademyProjectModels.KafkaModels;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using static MongoDB.Driver.WriteConcern;

namespace AcademyProjectSL.Services
{
    public class ConsumerService<TKey, TValue>
    {
        private readonly IOptionsMonitor<ISettings> _subSettings;
        private IConsumer<TKey, TValue> _consumer;
        private ConsumerConfig _consumerConfig;

        public ConsumerService(IOptionsMonitor<ISettings> subSettings)
        {
            _subSettings = subSettings;
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _subSettings.CurrentValue.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset)_subSettings.CurrentValue.AutoOffsetReset,
                GroupId = _subSettings.CurrentValue.GroupId,
            };
            _consumer = new ConsumerBuilder<TKey, TValue>(_consumerConfig).SetKeyDeserializer(new MessagePakcDeserializer<TKey>())
                .SetValueDeserializer(new MessagePakcDeserializer<TValue>()).Build();
            _consumer.Subscribe(_subSettings.CurrentValue.TopicName);
        }

        public ConsumeResult<TKey, TValue> Consume()
        {
            var cr = _consumer.Consume();
            return cr;
        }
    }
}
