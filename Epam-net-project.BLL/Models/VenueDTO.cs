using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.BLL.Models
{
    public class VenueDto
    {
        public int Id { get; set; }

        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string Phone { get; set; }
    }
}
