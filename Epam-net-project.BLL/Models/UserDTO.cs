using Microsoft.AspNet.Identity.EntityFramework;

namespace EpamNetProject.BLL.Models
{
    public class UserDTO : IdentityUser
    {
        public string Password { get; set; }

        public string Role { get; set; }

        public UserProfileDTO UserProfile { get; set; }
    }
}