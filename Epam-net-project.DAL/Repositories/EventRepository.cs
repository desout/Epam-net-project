using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.Models;

namespace EpamNetProject.DAL.Repositories
{
    public class EventRepository : IEventRepository
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
            var returnedId = _context.Database.SqlQuery<int>(
                    "EventInsert @Name, @Descr, @EventDate, @LayoutId, @ImgUrl",
                    new SqlParameter("@Name", entity.Name),
                    new SqlParameter("@Descr", entity.Description),
                    new SqlParameter("@EventDate", entity.EventDate),
                    new SqlParameter("@LayoutId", entity.LayoutId),
                    new SqlParameter("@ImgUrl", entity.ImgUrl))
                .FirstOrDefault();
            _context.SaveChanges();
            return returnedId;
        }

        public Event Get(int id)
        {
            return _context.Database.SqlQuery<Event>("EventSelectById @Id", new SqlParameter("@Id", id)).First();
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Database.SqlQuery<Event>("EventSelectAll");
        }

        public int Remove(int id)
        {
            _context.Database.ExecuteSqlCommand("EXEC EventDeleteById @Id", new SqlParameter("@Id", id));
            _context.SaveChanges();
            return id;
        }

        public int Update(Event entity)
        {
            _context.Database.ExecuteSqlCommand("EXEC EventUpdate @Name, @Descr, @EventDate, @LayoutId, @Id, @ImgUrl",
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Descr", entity.Description),
                new SqlParameter("@EventDate", entity.EventDate),
                new SqlParameter("@LayoutId", entity.LayoutId),
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@ImgUrl", entity.ImgUrl));
            _context.SaveChanges();
            return entity.Id;
        }
    }
}