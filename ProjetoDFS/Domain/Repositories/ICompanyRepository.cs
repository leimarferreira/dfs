using ProjetoDFS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> ListAsync();
        Task AddAsync(Company company);
        Task<Company> FindByIdAsync(int id);
        Task<Company> FindByCnpjAsync(string cnpj);
        void Update(Company company);
        void Remove(Company company);
    }
}
