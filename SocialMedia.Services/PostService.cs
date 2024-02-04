using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Data.Models;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public PostService(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task AddPostAsync(PostFormModel model, string userId)
        {
            string generatedGuid = Guid.NewGuid().ToString();
            string filesFolderPath = "files/" + generatedGuid + "_" + model.File.FileName;
            string fullPath = Path.Combine(webHostEnvironment.WebRootPath, filesFolderPath);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            Post post = new Post()
            {
                Text = model.Text,
                Path = filesFolderPath,
                UserId = userId
            };

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            Post postToDelete = await this.context.Posts.FindAsync(id);

            this.context.Posts.Remove(postToDelete!);

            string pathToDelete = Path.Combine(webHostEnvironment.WebRootPath, postToDelete!.Path);
            File.Delete(pathToDelete);

            await context.SaveChangesAsync();
        }

        public async Task EditPostAsync(int id, PostFormModel model)
        {
            Post post = await this.context.Posts.FindAsync(id);

            post!.Text = model.Text;

            await context.SaveChangesAsync();
        }

        public async Task<List<PostViewModel>> GetMyPostsAsync(int counter, string userId)
        {
            return await this.context.Posts
                .Where(p => p.UserId == userId)
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    Text = p.Text,
                    Path = p.Path,
                    UserId = p.UserId,
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                    Username = p.User.UserName!
                })
                .OrderByDescending(p => p.Id)
                .Skip(3 * (counter - 1 == -1 ? 0 : counter - 1))
                .Take(3)
                .ToListAsync();
        }

        public async Task<PostFormModel> GetPostByIdAsync(int id)
        {
            Post post = await this.context.Posts.FindAsync(id);

            return new PostFormModel()
            {
                Text = post!.Text
            };
        }

        public async Task<List<PostViewModel>> GetPostsAsync(int counter)
        {
            return await this.context.Posts.Select(p => new PostViewModel
            {
                Id = p.Id,
                Text = p.Text,
                Path = p.Path,
                UserId = p.UserId,
                FirstName = p.User.FirstName,
                LastName = p.User.LastName,
                Username = p.User.UserName!
            })
            .OrderByDescending(p => p.Id)
            .Skip(3 * (counter - 1 == -1 ? 0 : counter - 1))
            .Take(3)
            .ToListAsync();
        }

        public async Task<bool> ValidatePostUserAsync(string userId, int postId)
        {
            Post? post = await this.context.Posts.FindAsync(postId);

            if (post == null || post.UserId != userId)
            {
                return false;
            }

            return true;
        }
    }
}
