using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Controllers.API.Models;
using SocialMedia.Data;
using SocialMedia.Extensions;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Controllers.API
{
    [Route("api/posts")]
    [ApiController]
    public class PostsApiController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IPostService postService;
        public PostsApiController(ApplicationDbContext context, IPostService postService)
        {
            this.context = context;
            this.postService = postService;
        }

        public async Task<List<PostViewModel>> Posts(int counter)
        {
            return await this.context.Posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Text = p.Text,
                Path = p.Path,
                UserId = p.UserId,
                FirstName = p.User.FirstName,
                LastName = p.User.LastName,
                Username = p.User.UserName
            })
            .OrderByDescending(p => p.Id)
            .Skip(3 * (counter - 1 == -1 ? 0 : counter - 1 ))
            .Take(3)
            .ToListAsync();
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
