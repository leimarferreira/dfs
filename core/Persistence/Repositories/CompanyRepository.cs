using Microsoft.EntityFrameworkCore;
using core.Domain.Models;
using core.Domain.Repositories;
using core.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Persistence.Repositories
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
