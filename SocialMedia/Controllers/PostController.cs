using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Post;
using System.Security.Claims;

namespace SocialMedia.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            PostFormModel model = new PostFormModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await postService.AddPostAsync(model, currentUserId);

            return RedirectToAction("Index", "Home");
        }
    }
}
