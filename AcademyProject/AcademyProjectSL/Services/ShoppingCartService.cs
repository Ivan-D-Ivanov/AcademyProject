using AcademyProjectDL.DLInterfaces;
using AcademyProjectModels;
using AcademyProjectSL.Interfaces;

namespace AcademyProjectSL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IPurcahaseRepository _purchasesRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IPurcahaseRepository purchasesRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _purchasesRepository = purchasesRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task AddToCart(int userId, Book book)
        {
            var result = await _shoppingCartRepository.GetShoppingCart(userId);
            if (result != null)
            {
                result.Books.ToList().Add(book);
            }
            else
            {
                var shCart = new ShoppingCart()
                {
                    Id = Guid.NewGuid(),
                    Books = new List<Book>() { book },
                    UserId = userId
                };
                await _shoppingCartRepository.AddBookToCart(shCart);
            }
        }

        public Task EmptyCart(int id)
        {
            _shoppingCartRepository.EmptyCart(id);
            return Task.CompletedTask;
        }

        public Task FinishPurcahse(int userId)
        {
            var result = _shoppingCartRepository.GetShoppingCart(userId).Result;

            var purchase = new Purchase()
            {
                UserId = userId,
                Id = new Guid(),
                TotalMoney = result.Books.Sum(x => x.Price * x.Quantity),
                Books = result.Books.ToList()
            };

            _purchasesRepository.SavePurchase(purchase);
            return Task.CompletedTask;
        }

        public async Task<ShoppingCart> GetContent(int id)
        {
            var result = await _shoppingCartRepository.GetShoppingCart(id);
            return result;
        }

        public Task RemoveFromCart(int id, Book book)
        {
            
            return Task.CompletedTask;
        }
    }
}
