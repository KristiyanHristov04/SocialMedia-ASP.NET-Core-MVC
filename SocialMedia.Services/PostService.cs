using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Data.Models;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Post;

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
                UserId = userId,
                Date = DateTime.Now
            };

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfPostByUserIsLikedAsync(int id, string userId)
        {
            return await this.context.LikedPosts.AnyAsync(lp => lp.UserId == userId && lp.PostId == id);
        }

        public async Task LikeDislikePostAsync(int id, string userId)
        {
            bool isLiked = await CheckIfPostByUserIsLikedAsync(id, userId);

            if (isLiked)
            {
                LikedPost likedPostToRemove = await this.context.LikedPosts.Where(lp => lp.PostId == id && lp.UserId == userId).FirstOrDefaultAsync();
                this.context.LikedPosts.Remove(likedPostToRemove);
                await this.context.SaveChangesAsync();
            }
            else
            {
                LikedPost newLikedPost = new LikedPost()
                {
                    PostId = id,
                    UserId = userId
                };

                await this.context.LikedPosts.AddAsync(newLikedPost);
                await this.context.SaveChangesAsync();
            }
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
                    Username = p.User.UserName!,
                    DateSeconds = p.Date.Second,
                    DateMinutes = p.Date.Minute,
                    DateHours = p.Date.Hour,
                    DateDay = p.Date.Day,
                    DateMonth = p.Date.Month,
                    DateYear = p.Date.Year
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
                Username = p.User.UserName!,
                DateSeconds = p.Date.Second,
                DateMinutes = p.Date.Minute,
                DateHours = p.Date.Hour,
                DateDay = p.Date.Day,
                DateMonth = p.Date.Month,
                DateYear = p.Date.Year
            })
            .OrderByDescending(p => p.Id)
            .Skip(3 * (counter - 1 == -1 ? 0 : counter - 1))
            .Take(3)
            .ToListAsync();
        }

        public async Task<bool> ValidateIfPostExistsAsync(int postId)
        {
            Post? post = await this.context.Posts.FindAsync(postId);

            if (post == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidatePostUserAsync(string userId, int postId)
        {
            Post post = await this.context.Posts.FindAsync(postId);

            if (post!.UserId != userId)
            {
                return false;
            }

            return true;
        }

        public async Task<List<PostViewModel>> GetMyLikedPostsAsync(int counter, string userId)
        {
            var allLikedPosts = await this.context.LikedPosts
                .OrderByDescending(lp => lp.Id)
                .Where(lp => lp.UserId == userId)
                .Select(lp => lp.PostId)
                .ToListAsync();

            List<PostViewModel> posts =  await this.context.Posts
                .Where(p => allLikedPosts.Contains(p.Id))
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    Text = p.Text,
                    Path = p.Path,
                    UserId = p.UserId,
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                    Username = p.User.UserName!,
                    DateSeconds = p.Date.Second,
                    DateMinutes = p.Date.Minute,
                    DateHours = p.Date.Hour,
                    DateDay = p.Date.Day,
                    DateMonth = p.Date.Month,
                    DateYear = p.Date.Year
                })
                .Skip(3 * (counter - 1 == -1 ? 0 : counter - 1))
                .Take(3)
                .ToListAsync();

            List<PostViewModel> mostRecentlyLikedPosts = new List<PostViewModel>();

            foreach (var post in allLikedPosts)
            {
                mostRecentlyLikedPosts.Add(posts.First(p => p.Id == post));
            }

            return mostRecentlyLikedPosts;
        }
    }
}
