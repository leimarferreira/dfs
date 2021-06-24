using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
