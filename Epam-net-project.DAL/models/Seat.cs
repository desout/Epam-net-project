namespace EpamNetProject.DAL.Models
{
    public class Seat : BaseEntity
    {
        public int AreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }
    }
}