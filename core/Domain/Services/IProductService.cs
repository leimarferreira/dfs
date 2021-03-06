using core.Domain.Models;
using core.Domain.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();

        Task<Product> FindByIdAsync(int id);
        Task<IEnumerable<Product>> FindByCompanyIdAsync(int id);
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(int id);
    }
}
