using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.BLL.Models
{
    public class EventAreaDto
    {
        public int Id { get; set; }
        [Required] public int EventId { get; set; }
        [Required] public string Description { get; set; }

        [Required] public int CoordX { get; set; }

        [Required] public int CoordY { get; set; }
        [Required] public decimal Price { get; set; }
    }
}