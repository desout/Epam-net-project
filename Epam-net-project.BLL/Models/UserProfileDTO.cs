
using System;
using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.BLL.models
{
    public class UserProfileDTO
    {
        [Required]public int Id;
        [Required]public string TimeZone;
        [Required]public string Language;
        [Required]public string FirstName;
        [Required]public string Surname;
        [Required]public string UserId;
        [Required]public decimal Balance;
        public DateTime? ReserveDate { get; set; }
        

    }
}
