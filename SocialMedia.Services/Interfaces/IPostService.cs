using Microsoft.Data.SqlClient.DataClassification;
using SocialMedia.ViewModels.Post;

namespace SocialMedia.Services.Interfaces
{
    public interface IPostService
    {
        Task AddPostAsync(PostAddFormModel model, string userId);
        Task DeletePostAsync(int id);
        Task EditPostAsync(int id, PostEditFormModel model);
        Task<PostEditFormModel> GetPostByIdAsync(int id);
        Task<bool> ValidatePostUserAsync(string userId, int postId);
        Task<bool> ValidateIfPostExistsAsync(int postId);
        Task<List<PostViewModel>> GetPostsAsync(int counter);
        Task<List<PostViewModel>> GetMyPostsAsync(int counter, string userId);
        Task<bool> CheckIfPostByUserIsLikedAsync(int postId, string userId);
        Task LikeDislikePostAsync(int postId, string userId);
        Task<List<PostViewModel>> GetMyLikedPostsAsync(int counter, string userId);
        Task<List<ProfileViewModel>> GetProfilesAsync(string? search, int counter);
        Task<List<PostViewModel>> GetPostsByProfileAsync(int counter, string username);
        Task ReportPostAsync(int id);
        Task<PostViewModel> GetReportPostAsync(int id);
        Task DismissReportedPostAsync(int postId);
        Task IncreaseDeletedReportedPostsCountAsync();
        Task<List<string>> AllAdminIdsAsync();
        Task<bool> CheckIfUserExistsByUsernameAsync(string username);
    }
}
