using System.Threading.Tasks.Dataflow;
using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.ConfigurationSettings;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AcademyProjectSL.Services
{
    public class PurchaseDataflowService : IHostedService
    {
        private readonly ConsumerService<Guid, Purchase> _consumerService;
        private readonly IOptionsMonitor<GenericConsumerSettings> _subSettings;
        private readonly IBookInMemoryRepo _bookRepo;

        public PurchaseDataflowService(IBookInMemoryRepo bookRepo, IOptionsMonitor<GenericConsumerSettings> subSettings)
        {
            _subSettings = subSettings;
            _consumerService = new ConsumerService<Guid, Purchase>(_subSettings);
            _bookRepo = bookRepo;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var bufferBlock = new BufferBlock<ConsumeResult<Guid, Purchase>>();
            var transformBlock = new TransformBlock<ConsumeResult<Guid, Purchase>, List<Book>>(tb =>
            {
                var books = tb.Message.Value.Books.ToList();
                return books;
            });
            var actionBlock = new ActionBlock<List<Book>>(async b =>
            {
                foreach (var item in b)
                {
                    var book = _bookRepo.GetById(item.Id);
                    if (book == null)
                    {
                        await _bookRepo.AddBook(book.Result);
                    }

                    if (book.Result.Quantity - item.Quantity < 0)
                    {
                        book.Result.Quantity = 0;
                        await _bookRepo.UpdateBook(book.Result);
                        continue;
                    }

                    book.Result.Quantity -= item.Quantity;
                    await _bookRepo.UpdateBook(book.Result);
                }
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
            throw new NotImplementedException();
        }
    }
}
