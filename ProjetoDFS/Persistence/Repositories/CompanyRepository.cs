using Microsoft.EntityFrameworkCore;
using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Repositories;
using ProjetoDFS.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDFS.Persistence.Repositories
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Company>> ListAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> FindByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company> FindByCnpjAsync(string cnpj)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Cnpj == cnpj);
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public void Update(Company company)
        {
            _context.Companies.Update(company);
        }

        public void Remove(Company company)
        {
            _context.Companies.Remove(company);
        }
    }
}
