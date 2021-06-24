using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Services
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Purchase>> ListAsync();
        Task<Purchase> FindByIdAsync(int id);
        Task<IEnumerable<Purchase>> FindByUserIdAsync(int id);
        Task<PurchaseResponse> SaveAsync(Purchase purchase);
        Task<PurchaseResponse> UpdateAsync(int id, Purchase purchase);
        Task<PurchaseResponse> DeleteAsync(int id);
    }
}
