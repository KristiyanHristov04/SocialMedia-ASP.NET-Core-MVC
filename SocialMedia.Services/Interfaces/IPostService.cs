using SocialMedia.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services.Interfaces
{
    public interface IPostService
    {
        Task AddPostAsync(PostFormModel model, string userId);
        Task DeletePostAsync(int id);
        Task EditPostAsync(int id, PostFormModel model);
        Task<PostFormModel> GetPostByIdAsync(int id);
        Task<bool> ValidatePostUserAsync(string userId, int postId);
        Task<List<PostViewModel>> GetPostsAsync(int counter);
        Task<List<PostViewModel>> GetMyPostsAsync(int counter, string userId);
    }
}
