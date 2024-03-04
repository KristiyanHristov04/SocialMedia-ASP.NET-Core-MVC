using Microsoft.AspNetCore.Mvc;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.User;

namespace SocialMedia.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
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
    }
}
