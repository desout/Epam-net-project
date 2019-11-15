using System;
using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Models
{
    public class EditEventViewModel
    {
        [Required] public string Description;

        public int? Id;

        [Required] public string imgUrl;

        [Required] public int Layout;

        [Required] public DateTime Time;

        [Required] public string Title;
    }
}