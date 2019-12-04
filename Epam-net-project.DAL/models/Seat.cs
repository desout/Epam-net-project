using EpamNetProject.DAL.Models;

namespace EpamNetProject.DAL.models
{
    public class Seat : BaseEntity
    {
        public int AreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }
    }
}