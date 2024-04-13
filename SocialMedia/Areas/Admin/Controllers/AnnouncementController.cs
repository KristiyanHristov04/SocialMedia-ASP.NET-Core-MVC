using Microsoft.AspNetCore.Mvc;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Announcement;
using SocialMedia.Extensions;

namespace SocialMedia.Areas.Admin.Controllers
{
    public class AnnouncementController : AdminController
    {
        private readonly IAnnouncementService announcementService;
        public AnnouncementController(IAnnouncementService announcementService)
        {
            this.announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnnouncementCreateFormModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            string currentUserId = this.User.GetUserId();

            await this.announcementService.CreateAnnouncementAsync(currentUserId, model);
            return RedirectToAction(nameof(All));
        }
    }
}
