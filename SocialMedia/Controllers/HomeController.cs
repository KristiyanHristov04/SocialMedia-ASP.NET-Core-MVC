using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Email;
using System.Diagnostics;
using System.Security.Claims;

namespace SocialMedia.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomEmailSender customEmailSender;

        public HomeController(
            ILogger<HomeController> logger,
            ICustomEmailSender customEmailSender)
        {
            _logger = logger;
            this.customEmailSender = customEmailSender;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Terms()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Contact()
        {
            ContactFormModel model = new ContactFormModel();
            if (this.User.Identity?.IsAuthenticated ?? false)
            {
                string currentUserEmailAddress = this.User.FindFirst(ClaimTypes.Email)!.Value;
                model.FromEmail = currentUserEmailAddress;
            }
            
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                string currentUserEmailAddress = this.User.FindFirst(ClaimTypes.Email)!.Value;
                model.FromEmail = currentUserEmailAddress;

                return View(model);
            }

            bool isSent = await this.customEmailSender.SendEmailAsync(model.FromEmail, model.Subject, model.Message);

            if(!this.User.Identity?.IsAuthenticated ?? false)
            {
                if (isSent)
                {
                    TempData["EmailSentAnonymous"] = "Email sent successfully!";
                }
                else
                {
                    TempData["EmailNotSentAnonymous"] = "Something went wrong!";
                }

                return RedirectToAction(nameof(Contact));
            }

            if (isSent)
            {
                TempData["EmailSent"] = "Email sent successfully!";
            }
            else
            {
                TempData["EmailNotSent"] = "Something went wrong!";
            }

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error400");
            }
            else if (statusCode == 404)
            {
                return View("Error404");
            }
            else if (statusCode == 500)
            {
                return View("Error500");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
