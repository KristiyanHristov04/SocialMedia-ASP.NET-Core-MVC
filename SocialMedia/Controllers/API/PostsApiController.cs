﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Extensions;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Post;

namespace SocialMedia.Controllers.API
{
    [Route("api/posts")]
    [ApiController]
    public class PostsApiController : ControllerBase
    {
        private readonly IPostService postService;
        public PostsApiController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public async Task<List<PostViewModel>> Posts(int counter)
        {
            return await postService.GetPostsAsync(counter);
        }

        [Route("mine")]
        [HttpGet]
        public async Task<List<PostViewModel>> MyPosts(int counter)
        {
            string currentUserId = this.User.GetUserId();
            return await postService.GetMyPostsAsync(counter, currentUserId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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

            await this.postService.DeletePostAsync(id);

            return Ok();
        }
    }
}
