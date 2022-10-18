using AcademyProjectModels;

namespace AcademyProjectSL.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase?> SavePurchase(Purchase purchase);
        Task<Purchase?> DeletePurchase(Purchase purchase);
        Task<IEnumerable<Purchase>> GetPurchases(int id);
    }
}
