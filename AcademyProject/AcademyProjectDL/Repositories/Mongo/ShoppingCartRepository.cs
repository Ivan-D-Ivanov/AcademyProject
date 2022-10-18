using AcademyProjectDL.DLInterfaces;
using AcademyProjectModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AcademyProjectDL.Repositories.Mongo
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ILogger<ShoppingCartRepository> _logger;
        private readonly IOptionsMonitor<MongoDbConfiguration> _optionsMonitor;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ShoppingCart> _mongoCollection;

        public ShoppingCartRepository(IOptionsMonitor<MongoDbConfiguration> optionsMonitor, ILogger<ShoppingCartRepository> logger)
        {
            _optionsMonitor = optionsMonitor;
            _mongoClient = new MongoClient(_optionsMonitor.CurrentValue.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(_optionsMonitor.CurrentValue.DatabaseName);
            _mongoCollection = _mongoDatabase.GetCollection<ShoppingCart>("ShoppingCart");
            _logger = logger;
        }

        public async Task<ShoppingCart> AddBookToCart(ShoppingCart shoppingCart)
        {
            try
            {
                var result = _mongoCollection.InsertOneAsync(shoppingCart);
                return shoppingCart;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task EmptyCart(int userId)
        {
            try
            {
                var result = await _mongoCollection.DeleteOneAsync(x => x.UserId.Equals(userId));
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public async Task<ShoppingCart> GetShoppingCart(int userId)
        {
            var result = await _mongoCollection.FindAsync(x => x.UserId.Equals(userId)).Result.SingleOrDefaultAsync();
            return result;
        }
    }
}
