using core.Domain.Models;
using core.Domain.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Domain.Services
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
