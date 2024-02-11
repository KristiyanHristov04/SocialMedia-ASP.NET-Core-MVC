using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Extensions;
using SocialMedia.Models;
using SocialMedia.ViewModels.Home;
using System.Diagnostics;
using System.Security.Claims;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Controllers
{
    [Authorize]
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

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ContactFormModel model = new ContactFormModel();
            string currentUserEmailAddress = this.User.FindFirst(ClaimTypes.Email)!.Value;
            model.FromEmail = currentUserEmailAddress;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                string currentUserEmailAddress = this.User.FindFirst(ClaimTypes.Email)!.Value;
                model.FromEmail = currentUserEmailAddress;

                return View(currentUserEmailAddress);
            }

            bool isSent = await this.customEmailSender.SendEmailAsync(model.FromEmail, model.Subject, model.Message);

            if (isSent)
            {
                TempData["EmailSent"] = "Email sent successfully!";
            }
            else
            {
                TempData["EmailNotSent"] = "Something went wrong with sending the email! Try again.";
            }

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
