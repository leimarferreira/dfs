using ProjetoDFS.Domain.Repositories;
using ProjetoDFS.Persistence.Contexts;
using System.Threading.Tasks;

namespace ProjetoDFS.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
