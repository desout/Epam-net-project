﻿namespace EpamNetProject.DAL.Models
{
    public class Area : BaseEntity
    {
        public int LayoutId { get; set; }

        public string Description { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }

        public decimal Price { get; set; }
    }
}