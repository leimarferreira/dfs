using Microsoft.EntityFrameworkCore;
using core.Domain.Models;
using core.Domain.Repositories;
using core.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products.Include(p => p.Company).ToListAsync();
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Products.Include(p => p.Company).FirstAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return await _context.Products.FindAsync(id);
            }
        }

        public async Task<IEnumerable<Product>> FindByCompanyIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Company).Where(p => p.Company.Id == id).ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            var company = _context.Companies.Find(product.Company.Id);
            product.Company = company;
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            var company = _context.Companies.Find(product.Company.Id);
            product.Company = company;
            _context.Products.Update(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
