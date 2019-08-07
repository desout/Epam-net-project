using System;
using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.BLL.Models
{
    public class EventDto
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int LayoutId { get; set; }
        [Required] public DateTime EventDate { get; set; }
    }
}