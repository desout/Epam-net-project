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

        public int? CoordX { get; set; }

        public int? CoordY { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}