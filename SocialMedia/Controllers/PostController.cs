﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Extensions;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Post;

namespace SocialMedia.Controllers
{
    [Authorize(Roles = "User")]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            PostAddFormModel model = new PostAddFormModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostAddFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string currentUserId = this.User.GetUserId();
            await postService.AddPostAsync(model, currentUserId);

            TempData["SuccessAdd"] = "Post added successfully!";

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.postService.ValidateIfPostExistsAsync(id))
            {
                return BadRequest();
            }

            string currentUserId = this.User.GetUserId();

            if (!await postService.ValidatePostUserAsync(currentUserId, id))
            {
                return Unauthorized();
            }

            PostEditFormModel post = await this.postService.GetPostByIdAsync(id);

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostEditFormModel model)
        {
            if (!await this.postService.ValidateIfPostExistsAsync(id))
            {
                return BadRequest();
            }

            string currentUserId = this.User.GetUserId();

            if (!await postService.ValidatePostUserAsync(currentUserId, id))
            {
                return Unauthorized();
            }

            await this.postService.EditPostAsync(id, model);

            TempData["SuccessEdit"] = "Post edited successfully!";

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public IActionResult Mine()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Liked()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profiles(string search)
        {
            ViewData["Search"] = search;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string username)
        {
            if(!await this.postService.CheckIfUserExistsByUsernameAsync(username))
            {
                return BadRequest();
            }

            if (this.User.Identity?.Name == username)
            {
                return RedirectToAction(nameof(Mine));
            }

            ViewData["Username"] = username;
            return View();
        }
    }
}
