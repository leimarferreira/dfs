using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> ListAsync();
        Task<SaveCompanyResponse> SaveAsync(Company company);
    }
}
