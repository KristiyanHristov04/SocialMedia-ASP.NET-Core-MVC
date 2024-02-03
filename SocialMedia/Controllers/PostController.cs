using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.ViewModels.Post;
using System.Security.Claims;

namespace SocialMedia.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            PostFormModel model = new PostFormModel();
            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            model.UserId = currentUserId;
            return View(model);
        }
    }
}
