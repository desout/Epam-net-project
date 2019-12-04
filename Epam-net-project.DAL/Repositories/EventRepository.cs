using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;

namespace EpamNetProject.DAL.Repositories
{
    public class EventRepository : IRepository<Event>
    {
        internal MyContext _context;

        internal DbSet<Event> _dbSet;

        public EventRepository(MyContext context)
        {
            _context = context;
            _dbSet = _context.Set<Event>();
        }

        public int Add(Event entity)
        {
            var returnedId = int.Parse(_dbSet.SqlQuery("exec EventInsert @Name @Descr @EventDate @LayoutId",
                    new SqlParameter("@Name", entity.Name), new SqlParameter("@Descr", entity.Description),
                    new SqlParameter("@EventDate", entity.EventDate), new SqlParameter("@LayoutId", entity.LayoutId))
                .First().ToString());
            _context.SaveChanges();
            return returnedId;
        }

        public Event Get(int id)
        {
            return _dbSet.SqlQuery("exec EventSelectById @Id", new SqlParameter("@Id", id)).First();
        }

        public IEnumerable<Event> GetAll()
        {
            return _dbSet.SqlQuery("exec EventSelectAll");
        }

        public int Remove(int id)
        {
            _context.SaveChanges();
            return id;
        }

        public int Update(Event entity)
        {
            _dbSet.SqlQuery("exec EventUpdate @Name @Descr @EventDate @LayoutId @Id",
                new SqlParameter("@Name", entity.Name), new SqlParameter("@Descr", entity.Description),
                new SqlParameter("@EventDate", entity.EventDate), new SqlParameter("@LayoutId", entity.LayoutId),
                new SqlParameter("@Id", entity.Id));
            _context.SaveChanges();
            return entity.Id;
        }
    }
}