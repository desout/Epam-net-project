using System;

namespace EpamNetProject.DAL.Models
{
    public class UserProfile : BaseEntity
    {
        public decimal Balance { get; set; }

        public string FirstName { get; set; }

        public string Language { get; set; }

        public string Surname { get; set; }

        public string TimeZone { get; set; }

        public string UserId { get; set; }

        public DateTime? BasketTime { get; set; }
    }
}