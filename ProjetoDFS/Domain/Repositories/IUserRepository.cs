using ProjetoDFS.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task AddAsync(User user);
        Task<User> FindByIdAsync(int id);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByCpfAsync(string cpf);
        void Update(User user);
        void Remove(User user);
        Task<User> FirstOrDefaultAsync(string login, string password);
    }
}
