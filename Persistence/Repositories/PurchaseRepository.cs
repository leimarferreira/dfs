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
            return await _context.Purchases.ToListAsync();
        }

        public async Task AddAsync(Purchase purchase)
        {
            await _context.Purchases.AddAsync(purchase);
        }

        public async Task<Purchase> FindByIdAsync(int id)
        {
            return await _context.Purchases.FindAsync(id);
        }

        public async Task<IEnumerable<Purchase>> FindByUserIdAsync(int id)
        {
            return await _context.Purchases
                .Include(p => p.Buyer).Where(p => p.Buyer.Id == id).ToListAsync();
        }

        public void Update(Purchase purchase)
        {
            _context.Purchases.Update(purchase);
        }

        public void Remove(Purchase purchase)
        {
            _context.Purchases.Remove(purchase);
        }
    }
}
