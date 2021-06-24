using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<User> FindByIdAsync(int id);
        Task<UserResponse> FindByCredentials(string email, string password);
        Task<UserResponse> SaveAsync(User user);
        Task<UserResponse> UpdateAsync(int id, User user);
        Task<UserResponse> DeleteAsync(int id);
    }
}
