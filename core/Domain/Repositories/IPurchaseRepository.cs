using core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Domain.Repositories
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> ListAsync();
        Task AddAsync(Purchase purchase);
        Task<Purchase> FindByIdAsync(int id);
        Task<IEnumerable<Purchase>> FindByUserIdAsync(int id);
        void Update(Purchase purchase);
        void Remove(Purchase purchase);
    }
}
