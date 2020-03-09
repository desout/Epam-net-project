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
        internal MyContext Context;

        internal DbSet<Event> DbSet;

        public EventRepository(MyContext context)
        {
            Context = context;
            DbSet = Context.Set<Event>();
        }

        public int Add(Event entity)
        {
            var returnedId = Context.Database.SqlQuery<int>(
                    "EventInsert @Name, @Descr, @EventDate, @LayoutId, @ImgUrl",
                    new SqlParameter("@Name", entity.Name),
                    new SqlParameter("@Descr", entity.Description),
                    new SqlParameter("@EventDate", entity.EventDate),
                    new SqlParameter("@LayoutId", entity.LayoutId),
                    new SqlParameter("@ImgUrl", entity.ImgUrl))
                .FirstOrDefault();
            Context.SaveChanges();
            return returnedId;
        }

        public Event Get(int id)
        {
            return Context.Database.SqlQuery<Event>("EventSelectById @Id", new SqlParameter("@Id", id)).First();
        }

        public IEnumerable<Event> GetAll()
        {
            return Context.Database.SqlQuery<Event>("EventSelectAll");
        }

        public int Remove(int id)
        {
            Context.Database.ExecuteSqlCommand("EXEC EventDeleteById @Id", new SqlParameter("@Id", id));
            Context.SaveChanges();
            return id;
        }

        public int Update(Event entity)
        {
            Context.Database.ExecuteSqlCommand("EXEC EventUpdate @Name, @Descr, @EventDate, @LayoutId, @Id, @ImgUrl",
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Descr", entity.Description),
                new SqlParameter("@EventDate", entity.EventDate),
                new SqlParameter("@LayoutId", entity.LayoutId),
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@ImgUrl", entity.ImgUrl));
            Context.SaveChanges();
            return entity.Id;
        }
    }
}
