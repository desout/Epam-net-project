using System.Collections.Generic;
using System.Threading.Tasks;
using EpamNetProject.DAL.models;

namespace EpamNetProject.DAL.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
         Task Create(TEntity role);
         
         Task Update(TEntity role);

         Task Delete(TEntity role);

         Task<TEntity> Get(string id);

         Task<IEnumerable<TEntity>> GetAll();
    }
}
