using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Models
{
    public class LayoutViewModel
    {
        public int? Id { get; set; }

        [Required]
        public int VenueId { get; set; }

        [Required]
        public string LayoutName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}