using System.Collections.Generic;
using EpamNetProject.DAL.Models;

namespace EpamNetProject.DAL.Interfaces
{
    public interface IEventRepository
    {
        Event Get(int id);

        IEnumerable<Event> GetAll();

        int Add(Event entity);

        int Remove(int id);

        int Update(Event entity);
    }
}