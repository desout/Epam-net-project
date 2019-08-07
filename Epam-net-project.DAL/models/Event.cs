using System;

namespace EpamNetProject.DAL.models
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LayoutId { get; set; }
        public DateTime EventDate { get; set; }
    }
}
