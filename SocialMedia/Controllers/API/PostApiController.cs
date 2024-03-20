using Microsoft.AspNetCore.Mvc;
using SocialMedia.Extensions;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Post;

namespace SocialMedia.Controllers.API
{
    [Route("api/posts")]
    [ApiController]
    public class PostApiController : ControllerBase
    {
        private readonly IPostService postService;
        public PostApiController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostViewModel>>> Posts(int counter)
        {
            return Ok(await postService.GetPostsAsync(counter));
        }

        [Route("mine")]
        [HttpGet]
        public async Task<ActionResult<List<PostViewModel>>> MyPosts(int counter)
        {
            string currentUserId = this.User.GetUserId();
            return Ok(await postService.GetMyPostsAsync(counter, currentUserId));
        }

        [Route("liked")]
        [HttpGet]
        public async Task<ActionResult<List<PostViewModel>>> MyLikedPosts(int counter)
        {
            string currentUserId = this.User.GetUserId();
            return Ok(await postService.GetMyLikedPostsAsync(counter, currentUserId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.postService.ValidateIfPostExistsAsync(id))
            {
                return BadRequest();
            }

            string currentUserId = this.User.GetUserId();

            if (!this.User.IsInRole("Administrator") && !this.User.IsInRole("SuperAdministrator") && !await postService.ValidatePostUserAsync(currentUserId, id))
            {
                return Unauthorized();
            }

            await this.postService.DeletePostAsync(id);

            return Ok();
        }

        [HttpGet]
        [Route("isLiked/{id}")]
        public async Task<ActionResult<bool>> IsLiked(int id)
        {
            string currentUserId = this.User.GetUserId();

            return Ok(await this.postService.CheckIfPostByUserIsLikedAsync(id, currentUserId));
        }

        [HttpPost]
        [Route("like/{id}")]
        public async Task<IActionResult> LikeDislikePost(int id)
        {
            string currentUserId = this.User.GetUserId();

            await this.postService.LikeDislikePostAsync(id, currentUserId);

            return Ok();
        }

        [HttpGet]
        [Route("profiles")]
        public async Task<ActionResult<List<ProfileViewModel>>> Profiles(string? search, int counter)
        {
            return Ok(await this.postService.GetProfilesAsync(search, counter));
        }

        [Route("profile")]
        [HttpGet]
        public async Task<ActionResult<List<PostViewModel>>> Profile(int counter, string username)
        {
            return Ok(await postService.GetPostsByProfileAsync(counter, username));
        }

        [HttpPost("report/{id}")]
        public async Task<IActionResult> Report(int id)
        {
            await this.postService.ReportPostAsync(id);

            return Ok();
        }

        [HttpPost("report/dismiss/{id}")]
        public async Task<IActionResult> ReportDismiss(int id)
        {
            await this.postService.DismissReportedPostAsync(id);

            return Ok();
        }

        [HttpPost("statistics/reports/increase")]
        public async Task<IActionResult> DeletedReportedPostsCount()
        {
            await this.postService.IncreaseDeletedReportedPostsCountAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostViewModel>> Post(int id)
        {
            return Ok(await this.postService.GetReportPostAsync(id));
        }
    }
}
