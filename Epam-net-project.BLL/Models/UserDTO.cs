namespace EpamNetProject.BLL.Models
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public UserProfileDTO UserProfile { get; set; }
    }
}