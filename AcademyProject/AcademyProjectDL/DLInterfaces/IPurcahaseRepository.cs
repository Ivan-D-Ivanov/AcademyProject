using AcademyProjectModels;

namespace AcademyProjectDL.DLInterfaces
{
    public interface IPurcahaseRepository
    {
        Task<Purchase?> SavePurchase(Purchase purchase);
        Task<Purchase?> DeletePurchase(Purchase purchase);
        Task<IEnumerable<Purchase>> GetPurchases(int id);
    }
}
