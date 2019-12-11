using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Models
{
    public class UserProfileViewModel
    {
        [Required] public string Email { get; set; }

        [Required] public string FirstName{ get; set; }

        [Required] public string Language{ get; set; }

        [Required] public string Surname{ get; set; }

        [Required] public string TimeZone{ get; set; }

        [Required] public string UserId{ get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
