namespace EpamNetProject.DAL.models
{
    public class Seat : BaseEntity
    {
        public int AreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int Status { get; set; }
    }
}