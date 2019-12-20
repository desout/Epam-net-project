using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;

namespace EpamNetProject.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        internal MyContext _context;

        internal DbSet<TEntity> _dbSet;

        public Repository(MyContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public int Add(TEntity entity)
        {
            var item = _dbSet.Add(entity);
            _context.SaveChanges();
            return item.Id;
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public int Remove(int id)
        {
            var item = _dbSet.Find(id);
            var deletedItem = _dbSet.Remove(item);
            _context.SaveChanges();
            return deletedItem.Id;
        }

        public int Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity.Id;
        }
    }
}
