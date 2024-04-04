namespace SocialMedia.Areas.Admin.ViewModels.User
{
    public class AllViewModel
    {
        public List<UserViewModel> Users { get; set; } = null!;
        public const int UsersPerPage = 5;
        public int CurrentPage { get; set; } = 1;
        public string Filter { get; set; } = null!;
        public int TotalUsers { get; set; }
    }
}
