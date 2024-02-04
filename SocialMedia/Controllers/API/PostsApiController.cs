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
            .Skip(3 * (counter - 1))
            .Take(3)
            .ToListAsync();
        }
    }
}
