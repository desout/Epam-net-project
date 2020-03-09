using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.BLL.Models
{
    public class LayoutDto
    {
        public int Id { get; set; }

        [Required]
        public int VenueId { get; set; }

        [Required]
        public string LayoutName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}