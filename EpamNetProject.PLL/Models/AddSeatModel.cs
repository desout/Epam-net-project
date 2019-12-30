using System;
using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Models
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
