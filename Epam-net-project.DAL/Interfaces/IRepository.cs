using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        int Add(TEntity entity);
        int Remove(int id);
    }
}