using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Areas.Admin.ViewModels.Post;
using SocialMedia.Data;

namespace SocialMedia.Areas.Admin.Controllers
{
    public class PostController : AdminController
    {
        private readonly ApplicationDbContext context;
        public PostController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Preview(int id)
        {
            bool doesExist = await this.context.ReportPosts.AnyAsync(rp => rp.PostId == id);
            if (!doesExist)
            {
                return BadRequest();
            }

            string username = await this.context.Posts
                .Where(p => p.Id == id)
                .Select(p => p.User.UserName!)
                .FirstAsync();

            PreviewViewModel model = new PreviewViewModel
            {
                PostId = id,
                Username = username
            };

            return View(model);
        }
    }
}
