using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels.ConfigurationSettings;
using AcademyProjectModels;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Hosting;
using APIPurchaseDeliveryPublisher.Models;

namespace AcademyProjectSL.Services
{
    public class DeliveryDataFlowService : IHostedService
    {
        private readonly IOptionsMonitor<DeliveryConsumerSettings> _subSettings;
        private readonly IBookInMemoryRepo _bookRepo;
        private readonly ConsumerService<int, Delivery> _consumerService;

        public DeliveryDataFlowService(IBookInMemoryRepo bookRepo, IOptionsMonitor<DeliveryConsumerSettings> subSettings)
        {
            _bookRepo = bookRepo;
            _subSettings = subSettings;
            _consumerService = new ConsumerService<int, Delivery>(_subSettings);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var bufferBlock = new BufferBlock<ConsumeResult<int, Delivery>>();
            var transformBlock = new TransformBlock<ConsumeResult<int, Delivery>, Delivery>(tb =>
            {
                var book = tb.Message.Value;
                return book;
            });
            var actionBlock = new ActionBlock<Delivery>(async b =>
            {
                var tasks = new List<Task<Book>>();

                tasks.Add(Task.Run(async () =>
                {
                    var book = _bookRepo.GetById(b.Book.Id);
                    if (book == null)
                    {
                        return await _bookRepo.AddBook(book.Result);
                    }
                    else
                    {
                        book.Result.Quantity += b.Quantity;
                        return await _bookRepo.UpdateBook(book.Result);
                    }
                }));

                await Task.WhenAll(tasks);
            });

            bufferBlock.LinkTo(transformBlock);
            transformBlock.LinkTo(actionBlock);

            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    bufferBlock.Post(_consumerService.Consume());
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
