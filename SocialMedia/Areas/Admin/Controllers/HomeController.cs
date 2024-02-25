using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
