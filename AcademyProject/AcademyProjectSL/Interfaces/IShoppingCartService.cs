using AcademyProjectModels;

namespace AcademyProjectSL.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetContent(int id);
        Task AddToCart(int userId, Book book);
        Task RemoveFromCart(int id, Book book);
        Task EmptyCart(int id);
        Task FinishPurcahse(int purchaseId);
    }
}