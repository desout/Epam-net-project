using System;

namespace EpamNetProject.DAL.Models
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int LayoutId { get; set; }

        public DateTime EventDate { get; set; }

        public string ImgUrl { get; set; }
    }
}