namespace SocialMedia.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int DateSeconds { get; set; }
        public int DateMinutes { get; set; }
        public int DateHours { get; set; }
        public int DateDay { get; set; }
        public int DateMonth { get; set; }
        public int DateYear { get; set; }
    }
}
