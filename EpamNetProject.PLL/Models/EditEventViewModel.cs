using System;
using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Models
{
    public class EditEventViewModel
    {
        [Required]
        public string Description { get; set; }

        [Required] public string Name { get; set; }

        public int? Id { get; set; }

        [Required] public string ImgUrl { get; set; }

        [Required] public int Layout { get; set; }

        [Required] public DateTime Time { get; set; }

        [Required] public string Title { get; set; }
    }
}
