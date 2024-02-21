using Microsoft.AspNetCore.Mvc;
using SocialMedia.Extensions;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Post;

namespace SocialMedia.Controllers.API
{
    [Route("api/posts")]
    [ApiController]
    public class PostsApiController : ControllerBase
    {
        private readonly IPostService postService;
        public PostsApiController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public async Task<List<PostViewModel>> Posts(int counter)
        {
            return await postService.GetPostsAsync(counter);
        }

        [Route("mine")]
        [HttpGet]
        public async Task<List<PostViewModel>> MyPosts(int counter)
        {
            string currentUserId = this.User.GetUserId();
            return await postService.GetMyPostsAsync(counter, currentUserId);
        }

        [Route("liked")]
        [HttpGet]
        public async Task<List<PostViewModel>> MyLikedPosts(int counter)
        {
            string currentUserId = this.User.GetUserId();
            return await postService.GetMyLikedPostsAsync(counter, currentUserId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.postService.ValidateIfPostExistsAsync(id))
            {
                return BadRequest();
            }

            string currentUserId = this.User.GetUserId();

            if (!await postService.ValidatePostUserAsync(currentUserId, id))
            {
                return Unauthorized();
            }

            await this.postService.DeletePostAsync(id);

            return Ok();
        }

        [HttpGet]
        [Route("isLiked/{id}")]
        public async Task<bool> IsLiked(int id)
        {
            string currentUserId = this.User.GetUserId();

            return await this.postService.CheckIfPostByUserIsLikedAsync(id, currentUserId);
        }

        [HttpPost]
        [Route("like/{id}")]
        public async Task LikeDislikePost(int id)
        {
            string currentUserId = this.User.GetUserId();

            await this.postService.LikeDislikePostAsync(id, currentUserId);
        }

        [HttpGet]
        [Route("profiles")]
        public async Task<List<ProfileViewModel>> Profiles(string? search, int counter)
        {
            return await this.postService.GetProfilesAsync(search, counter);
        }

        [Route("profile")]
        [HttpGet]
        public async Task<List<PostViewModel>> Profile(int counter, string username)
        {
            return await postService.GetPostsByProfileAsync(counter, username);
        }
    }
}
