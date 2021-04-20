using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> ListAsync();
        Task<Company> FindByIdAsync(int id);
        Task<CompanyResponse> SaveAsync(Company company);
        Task<CompanyResponse> UpdateAsync(int id, Company company);
        Task<CompanyResponse> DeleteAsync(int id);
    }
}
