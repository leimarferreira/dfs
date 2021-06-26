using Microsoft.EntityFrameworkCore;
using core.Domain.Models;
using core.Domain.Repositories;
using core.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> FindByCpfAsync(string cpf)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Cpf == cpf);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<User> FirstOrDefaultAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
