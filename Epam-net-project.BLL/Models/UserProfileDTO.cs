using System;
using System.ComponentModel.DataAnnotations;

namespace EpamNetProject.BLL.Models
{
    public class UserProfileDTO
    {
        [Required]
        public decimal Balance { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string TimeZone { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime? basketTime { get; set; }
    }
}