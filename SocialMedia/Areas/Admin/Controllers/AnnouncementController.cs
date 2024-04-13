using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            List<AnnouncementViewModel> announcements
                = await this.announcementService.GetAnnouncementsAsync();

            return View(announcements);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnnouncementFormModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            string currentUserId = this.User.GetUserId();

            try
            {
                await this.announcementService.CreateAnnouncementAsync(currentUserId, model);
                TempData["SuccessAdd"] = "Announcement added successfully!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Something went wrong!";
            }
            
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.announcementService.CheckIfAnnouncementExistsById(id))
            {
                return BadRequest();
            }

            AnnouncementFormModel model
                = await this.announcementService.GetAnnouncementByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AnnouncementFormModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.announcementService.EditAnnouncementAsync(id, model);
                TempData["SuccessEdit"] = "Announcement edited successfully!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Something went wrong!";
            }
            

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.announcementService.CheckIfAnnouncementExistsById(id))
            {
                return BadRequest();
            }

            AnnouncementFormModel model
                = await this.announcementService.GetAnnouncementByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, AnnouncementFormModel model)
        {
            try
            {
                await this.announcementService.DeleteAnnouncementAsync(id, model);
                TempData["SuccessDelete"] = "Announcement deleted successfully!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Something went wrong!";
            }
            
            return RedirectToAction(nameof(All));
        }
    }
}
