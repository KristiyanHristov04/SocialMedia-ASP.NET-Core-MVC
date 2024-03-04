using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator,SuperAdministrator")]
    public class AdminController : Controller
    {
    }
}
