using AcademyProjectModels;

namespace AcademyProjectDL.DLInterfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCart(int userId);
        Task<ShoppingCart> AddBookToCart(ShoppingCart shoppingCart);
        Task EmptyCart(int userId);
    }
}
