namespace SocialMedia.Areas.Admin.ViewModels.User
{
    public class UserViewModel
    {
        public string UserId { get; set; } = null!;
        public string UserUsername { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserFullName { get; set; } = null!;
        public List<string> UserRoles { get; set; } = null!;
    }
}
