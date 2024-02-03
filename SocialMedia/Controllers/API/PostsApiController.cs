using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Controllers.API.Models;
using SocialMedia.Data;

namespace SocialMedia.Controllers.API
{
    [Route("api/posts")]
    [ApiController]
    public class PostsApiController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public PostsApiController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<PostViewModel>> Posts(int counter)
        {
            return await this.context.Posts.Select(x => new PostViewModel
            {
                Id = x.Id,
                Text = x.Text,
                Path = x.Path,
                UserId = x.UserId,
            })
            .Skip(3 * counter)
            .Take(3)
            .ToListAsync();
        }
    }
}
