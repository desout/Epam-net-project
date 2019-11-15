using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Models
{
    public class RegisterModel
    {
        [Required] public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required] public string UserName { get; set; }
        [Required] public string TimeZone { get; set; }
        [Required] public string Language { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string Surname { get; set; }
    }
}
