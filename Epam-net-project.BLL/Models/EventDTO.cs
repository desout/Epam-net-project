using System;

namespace EpamNetProject.BLL.Models
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LayoutId { get; set; }
        public DateTime EventDate { get; set; }
    }
}
