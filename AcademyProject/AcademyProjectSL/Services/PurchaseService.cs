using AcademyProjectDL.DLInterfaces;
using AcademyProjectModels;
using AcademyProjectSL.Interfaces;

namespace AcademyProjectSL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurcahaseRepository _purcahaseRepository;

        public PurchaseService(IPurcahaseRepository urcahaseRepository)
        {
            _purcahaseRepository = urcahaseRepository;
        }

        public async Task<Purchase?> DeletePurchase(Purchase purchase)
        {
            var result = await _purcahaseRepository.DeletePurchase(purchase);
            return result;
        }

        public async Task<IEnumerable<Purchase>> GetPurchases(int id)
        {
            var result = await _purcahaseRepository.GetPurchases(id);
            return result;
        }

        public async Task<Purchase> SavePurchase(Purchase purchase)
        {
            var result = await _purcahaseRepository.SavePurchase(purchase);
            return result;
        }
    }
}
