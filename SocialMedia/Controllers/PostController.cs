using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
