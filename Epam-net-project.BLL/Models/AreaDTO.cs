using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.BLL.Models
{
    public class AreaDto
    {
        public int Id { get; set; }

        [Required]
        public int LayoutId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CoordX { get; set; }

        [Required]
        public int CoordY { get; set; }

        [Required]
        public int Price { get; set; }
    }
}