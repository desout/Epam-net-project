using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Models
{
    public class UserProfileViewModel
    {
        [Required] public string TimeZone;
        [Required] public string Language;
        [Required] public string FirstName;
        [Required] public string Surname;
        [Required] public string UserId;
        [Required] public string Email;
        [DataType(DataType.Password)] public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
