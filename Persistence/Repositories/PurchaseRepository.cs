using Microsoft.EntityFrameworkCore;
using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Repositories;
using ProjetoDFS.Persistence.Contexts;
using System.Collections.Generic;
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
    }
}
