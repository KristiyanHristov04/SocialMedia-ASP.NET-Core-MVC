using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string currentUserId = this.User.GetUserId();
            if (!await postService.ValidatePostUserAsync(currentUserId, id))
            {
                return BadRequest();
            }

            await this.postService.DeletePostAsync(id);
            return Ok();
        }
    }
}
