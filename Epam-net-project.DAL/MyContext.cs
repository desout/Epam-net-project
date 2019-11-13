using System.Data.Entity;
using EpamNetProject.DAL.models;
using EpamNetProject.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EpamNetProject.DAL
{
    public class MyContext: IdentityDbContext<User>
    {
        public MyContext(string connectionString) : base(connectionString)
        { }
        
        public DbSet<Area> Areas { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventArea> EventAreas { get; set; }
        public DbSet<EventSeat> EventSeats { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Venue> Venues { get; set; }
    }
}
