using System;

namespace EpamNetProject.DAL.models
{
    public class UserProfile : BaseEntity
    {
        public decimal Balance;
        public string FirstName;
        public string Language;
        public string Surname;
        public string TimeZone;
        public string UserId;
        public DateTime? ReserveDate { get; set; }
    }
}