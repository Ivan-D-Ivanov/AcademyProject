using AcademyProjectModels;
using AcademyProjectModels.CongigurationSettings;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AcademyProjectModels.KafkaModels;
using System.Threading;

namespace AcademyProjectSL.BackgroundServ
{
    public class MyBackgroundService : IHostedService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private ConsumerConfig _consumerConfig;
        private readonly IOptionsMonitor<KafkaSubscriberSettings> _subSettings;
        private IConsumer<int, Person> _consumer;

        public MyBackgroundService(ILogger<MyBackgroundService> logger, IOptionsMonitor<KafkaSubscriberSettings> subSettings)
        {
            _logger = logger;
            _subSettings = subSettings;
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _subSettings.CurrentValue.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset)_subSettings.CurrentValue.AutoOffsetReset,
                GroupId = _subSettings.CurrentValue.GroupId,
            };

            _consumer = new ConsumerBuilder<int, Person>(_consumerConfig).SetKeyDeserializer(new MessagePakcDeserializer<int>())
                .SetValueDeserializer(new MessagePakcDeserializer<Person>()).Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe("ProducerPersonTopic");
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumer.Consume();
                    Console.WriteLine($"Recieved msg with id : {cr.Value.Id} name : {cr.Value.Name} age : {cr.Value.Age}");
                }
            },cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}