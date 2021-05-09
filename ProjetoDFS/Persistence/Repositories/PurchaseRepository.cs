using Microsoft.EntityFrameworkCore;
using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Repositories;
using ProjetoDFS.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Persistence.Repositories
{
    public class PurchaseRepository : BaseRepository, IPurchaseRepository
    {
        public PurchaseRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Purchase>> ListAsync()
        {
            return await _context.Purchases
                .Include(p => p.Buyer)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<Purchase> FindByIdAsync(int id)
        {
            return await _context.Purchases
                .Include(p => p.Buyer)
                .Include(p => p.Product)
                .FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Purchase>> FindByUserIdAsync(int id)
        {
            return await _context.Purchases
                .Include(p => p.Product)
                .Include(p => p.Buyer)
                .Where(p => p.Buyer.Id == id)
                .ToListAsync();
        }

        public async Task AddAsync(Purchase purchase)
        {
            var user = _context.Users.Find(purchase.Buyer.Id);
            var product = _context.Products.Find(purchase.Product.Id);
            purchase.Buyer = user;
            purchase.Product = product;
            await _context.Purchases.AddAsync(purchase);
        }

        public void Update(Purchase purchase)
        {
            var user = _context.Users.Find(purchase.Buyer.Id);
            var product = _context.Products.Find(purchase.Product.Id);
            purchase.Buyer = user;
            purchase.Product = product;
            _context.Purchases.Update(purchase);
        }

        public void Remove(Purchase purchase)
        {
            _context.Purchases.Remove(purchase);
        }
    }
}
