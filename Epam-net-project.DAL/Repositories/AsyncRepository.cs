using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EpamNetProject.DAL.Interfaces;

namespace EpamNetProject.DAL.Repositories
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        private readonly MyContext _context;

        private readonly DbSet<TEntity> _dbSet;

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
            _context.Entry(entity).State = EntityState.Modified;
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

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }
    }
}
