﻿namespace EpamNetProject.DAL.Models
{
    public class EventSeat : BaseEntity
    {
        public int EventAreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public SeatStatus State { get; set; }

        public string UserId { get; set; }
    }
}