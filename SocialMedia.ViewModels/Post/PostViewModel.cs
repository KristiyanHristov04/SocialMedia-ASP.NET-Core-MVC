namespace SocialMedia.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public int DateSeconds { get; set; }
        public int DateMinutes { get; set; }
        public int DateHours { get; set; }
        public int DateDay { get; set; }
        public int DateMonth { get; set; }
        public int DateYear { get; set; }
    }
}
