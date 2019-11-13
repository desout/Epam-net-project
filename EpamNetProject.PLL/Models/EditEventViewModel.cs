using System;
using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Models
{
    public class EditEventViewModel
    {
        public int? Id;
        [Required]
        public DateTime Time;
        [Required]
        public string Description;
        [Required]
        public string Title;
        [Required]
        public string imgUrl;
        [Required]
        public int Layout;

    }
}
