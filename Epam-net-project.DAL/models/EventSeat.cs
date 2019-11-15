using System;

namespace EpamNetProject.DAL.models
{
    public class EventSeat : BaseEntity
    {
        public int EventAreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int State { get; set; }
        public string UserId { get; set; }
        public DateTime ReserveDate { get; set; }
    }
}