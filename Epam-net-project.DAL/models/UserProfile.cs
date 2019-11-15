
using System;

namespace EpamNetProject.DAL.models
{
    public class UserProfile: BaseEntity
    {
        public string TimeZone;
        public string Language;
        public string FirstName;
        public string Surname;
        public string UserId;
        public decimal Balance;
        public DateTime? ReserveDate { get; set; }

    }
}
