﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Areas.Admin.Filters;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.User;
using SocialMedia.Data.Models;
using SocialMedia.Extensions;

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
            ApplicationUser? user = await this.userManager.FindByIdAsync(id);
            if (await this.userManager.IsInRoleAsync(user!, "Administrator"))
            {
                return Forbid();
            }

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
        [AdminActivityActionFilter(nameof(Promote))]
        public async Task<IActionResult> Promote(string id, UserViewModel model)
        {
            if (!await this.userService.CheckIfUserEligibleForPromoteAsync(id))
            {
                return Forbid();
            }

            ApplicationUser? user = await this.userManager.FindByIdAsync(id);

            if (await this.userManager.IsInRoleAsync(user!, "User"))
            {
                await this.userManager.RemoveFromRoleAsync(user!, "User");
            }

            await this.userManager.AddToRoleAsync(user!, "Administrator");

            TempData["Promoted"] = $"You promoted @{user!.UserName} to an admin!";

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [AdminActivityActionFilter(nameof(Demote))]
        public async Task<IActionResult> Demote(string id)
        {
            if (!this.User.IsInRole("SuperAdministrator"))
            {
                return Forbid();
            }

            try
            {
                UserViewModel userToDemote
                    = await this.userService.GetUserAsync(id);

                if (userToDemote.UserRole == "User")
                {
                    return BadRequest();
                }

                return View(userToDemote);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = $"Something went wrong!";
                return RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Demote(string id, UserViewModel model)
        {
            if (!this.User.IsInRole("SuperAdministrator"))
            {
                return Forbid();
            }

            if (model.UserRole == "User")
            {
                return BadRequest();
            }

            try
            {
                ApplicationUser user = await this.userManager.FindByIdAsync(id);

                if (await this.userManager.IsInRoleAsync(user!, "Administrator"))
                {
                    await this.userManager.RemoveFromRoleAsync(user!, "Administrator");
                }

                await this.userManager.AddToRoleAsync(user!, "User");

                TempData["Promoted"] = $"You demoted @{user!.UserName} to an user!";

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = $"Something went wrong!";
                return RedirectToAction(nameof(All));
            }
        }
    }
}
