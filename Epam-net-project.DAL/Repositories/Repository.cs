using System.Data.Entity;
using System.Linq;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.Models;

namespace EpamNetProject.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        internal MyContext Context;

        internal DbSet<TEntity> DbSet;

        public Repository(MyContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public int Add(TEntity entity)
        {
            var item = DbSet.Add(entity);
            Context.SaveChanges();
            return item.Id;
        }

        public TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public int Remove(int id)
        {
            var item = DbSet.Find(id);
            var deletedItem = DbSet.Remove(item);
            Context.SaveChanges();
            return deletedItem.Id;
        }

        public int Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity.Id;
        }
    }
}
