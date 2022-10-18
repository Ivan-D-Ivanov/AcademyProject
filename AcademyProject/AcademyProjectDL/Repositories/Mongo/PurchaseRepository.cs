using AcademyProjectDL.DLInterfaces;
using AcademyProjectModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AcademyProjectDL.Repositories.Mongo
{
    public class PurchaseRepository : IPurcahaseRepository
    {
        private readonly ILogger<PurchaseRepository> _logger;
        private readonly IOptionsMonitor<MongoDbConfiguration> _optionsMonitor;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<Purchase> _mongoCollection;

        public PurchaseRepository(IOptionsMonitor<MongoDbConfiguration> optionsMonitor, ILogger<PurchaseRepository> logger)
        {
            _optionsMonitor = optionsMonitor;
            _mongoClient = new MongoClient(_optionsMonitor.CurrentValue.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(_optionsMonitor.CurrentValue.DatabaseName);
            _mongoCollection = _mongoDatabase.GetCollection<Purchase>("Purchase");
            _logger = logger;
        }

        public async Task<Purchase> DeletePurchase(Purchase purchase)
        {
            try
            {
                await _mongoCollection.DeleteOneAsync(x => x.UserId == purchase.UserId);
                return purchase;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Purchase>> GetPurchases(int id)
        {
            try
            {
                var result = await _mongoCollection.FindAsync(x => x.UserId == id);
                return await result.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Purchase?> SavePurchase(Purchase purchase)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(purchase);
                return purchase;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
