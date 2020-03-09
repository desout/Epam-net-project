using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Utils.Models
{
    public class AddSeatModel
    {
        [Required]
        public int Row { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int AreaId { get; set; }
    }
}