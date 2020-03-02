using System.Linq;
using System.Threading.Tasks;

namespace EpamNetProject.DAL.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task Create(TEntity role);

        Task Update(TEntity role);

        Task Delete(TEntity role);

        Task<TEntity> Get(string id);

        IQueryable<TEntity> GetAll();
    }
}
