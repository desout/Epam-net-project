﻿namespace EpamNetProject.DAL.Models
{
    public class Venue : BaseEntity
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}