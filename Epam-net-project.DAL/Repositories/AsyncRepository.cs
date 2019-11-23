using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;

namespace EpamNetProject.DAL.Repositories
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        internal MyContext _context;

        internal DbSet<TEntity> _dbSet;

        public AsyncRepository(MyContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task Create(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<TEntity> Get(string id)
        {
            return _dbSet.FindAsync(id);
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            return Task.FromResult((IEnumerable<TEntity>) _dbSet);
        }
    }
}
