using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Models
{
    public class AddAreaModel
    {
        [Required]
        public Corner LeftCorner { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int EventId { get; set; }
    }

    public class Corner
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}