using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Extensions;
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
            if (model.File.Length > 1000000)
            {
                ModelState.AddModelError(string.Empty, "Max file size is 1 MB.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string currentUserId = this.User.GetUserId();
            await postService.AddPostAsync(model, currentUserId);

            return RedirectToAction("Index", "Home");
        }
    }
}
