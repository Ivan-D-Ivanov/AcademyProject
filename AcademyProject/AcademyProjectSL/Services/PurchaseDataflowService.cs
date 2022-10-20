using System.Threading.Tasks.Dataflow;
using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectModels.ConfigurationSettings;
using AcademyProjectSL.ServiceProviders;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AcademyProjectSL.Services
{
    public class PurchaseDataflowService : IHostedService
    {
        private readonly ConsumerService<Guid, Purchase> _consumerService;
        private readonly IOptionsMonitor<GenericConsumerSettings> _subSettings;
        private readonly IBookInMemoryRepo _bookRepo;
        private readonly PurchaseServiceProvider _purchaseProvider;

        public PurchaseDataflowService(IBookInMemoryRepo bookRepo, IOptionsMonitor<GenericConsumerSettings> subSettings, PurchaseServiceProvider purchaseProvider)
        {
            _subSettings = subSettings;
            _consumerService = new ConsumerService<Guid, Purchase>(_subSettings);
            _bookRepo = bookRepo;
            _purchaseProvider = purchaseProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var bufferBlock = new BufferBlock<ConsumeResult<Guid, Purchase>>();
            var transformBlock = new TransformBlock<ConsumeResult<Guid, Purchase>, List<Book>>(tb =>
            {
                var addInfo = _purchaseProvider.GetAdditionalInfo().Result;
                foreach (var info in addInfo.Contents)
                {
                    tb.Message.Value.AdditionalInfo.ToList().Add(info);
                }
                var books = tb.Message.Value.Books.ToList();
                return books;
            });
            var actionBlock = new ActionBlock<List<Book>>(async b =>
            {
                var tasks = new List<Task<Book>>();
                foreach (var item in b)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        var book = await _bookRepo.GetById(item.Id);
                        if (book == null)
                        {
                            return await _bookRepo.AddBook(item);
                        }

                        if (book.Quantity - item.Quantity < 0)
                        {
                            book.Quantity = 0;
                            return await _bookRepo.UpdateBook(book);
                        }

                        book.Quantity -= item.Quantity;
                        
                        return await _bookRepo.UpdateBook(book);
                    }));
                }

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
