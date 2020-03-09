using Microsoft.AspNet.Identity.EntityFramework;

namespace EpamNetProject.BLL.Models
{
    public class UserDto : IdentityUser
    {
        public string Password { get; set; }

        public string Role { get; set; }

        public UserProfileDto UserProfile { get; set; }
    }
}
