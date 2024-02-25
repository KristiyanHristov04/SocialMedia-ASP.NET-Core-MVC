using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Areas.Admin.Controllers
{
    public class PostController : AdminController
    {
        [HttpGet]
        public IActionResult Preview(int id)
        {
            ViewData["PostId"] = id;
            return View();
        }
    }
}
