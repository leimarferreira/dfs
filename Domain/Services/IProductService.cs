using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<SaveProductResponse> SaveAsync(Product product);
    }
}
