﻿namespace EpamNetProject.DAL.Models
{
    public class Layout : BaseEntity
    {
        public int VenueId { get; set; }

        public string LayoutName { get; set; }

        public string Description { get; set; }
    }
}