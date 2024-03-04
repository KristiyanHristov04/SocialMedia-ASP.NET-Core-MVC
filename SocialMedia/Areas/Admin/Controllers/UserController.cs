using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.User;
using SocialMedia.Data.Models;

namespace SocialMedia.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        public UserController(
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> All(AllViewModel allModel)
        {
            if (allModel.CurrentPage <= 0)
            {
                allModel.CurrentPage = 1;
            }

            AllViewModel model
                = await this.userService
                .GetUsersAsync(allModel.Filter, allModel.CurrentPage);

            allModel.TotalUsers = model.TotalUsers;
            allModel.Users = model.Users;

            return View(allModel);
        }

        [HttpGet]
        public async Task<IActionResult> Promote(string id)
        {
            UserViewModel userToPromote
                = await this.userService.GetUserAsync(id);

            if (await this.userService.CheckIfUserEligibleForPromoteAsync(id))
            {
                ViewData["IsEligible"] = true;
            }
            else
            {
                ViewData["IsEligible"] = false;
            }

            return View(userToPromote);
        }

        [HttpPost]
        public async Task<IActionResult> Promote(string id, UserViewModel model)
        {
            ApplicationUser? user = await this.userManager.FindByIdAsync(id);

            if (await this.userManager.IsInRoleAsync(user!, "User"))
            {
                await this.userManager.RemoveFromRoleAsync(user!, "User");
            }

            await this.userManager.AddToRoleAsync(user!, "Administrator");

            return RedirectToAction(nameof(All));
        }
    }
}
