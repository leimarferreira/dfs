using ProjetoDFS.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<Product> FindByIdAsync(int id);
        Task<IEnumerable<Product>> FindByCompanyIdAsync(int id);
        Task AddAsync(Product product);

        void Update(Product product);
        void Remove(Product product);
    }
}
