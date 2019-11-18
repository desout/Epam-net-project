using System;
using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.PLL.Models
{
    public class UserProfileDTO
    {
        [Required] public decimal Balance;
        [Required] public string FirstName;
        [Required] public int Id;
        [Required] public string Language;
        [Required] public string Surname;
        [Required] public string TimeZone;
        [Required] public string UserId;
        public DateTime? ReserveDate { get; set; }
    }
}
