using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;
        public PostService(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
        }

        public async Task AddPostAsync(PostAddFormModel model, string userId)
        {
            string generatedGuid = Guid.NewGuid().ToString();
            string filesFolderPath = "files/" + generatedGuid + "_" + model.File.FileName;
            string fullPath = Path.Combine(webHostEnvironment.WebRootPath, filesFolderPath);

            string folderPath = Path.Combine(webHostEnvironment.WebRootPath, "files");
            Directory.CreateDirectory(folderPath);

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
                LikedPost likedPostToRemove = await this.context.LikedPosts.Where(lp => lp.PostId == id && lp.UserId == userId).FirstAsync();
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

        public async Task EditPostAsync(int id, PostEditFormModel model)
        {
            Post? post = await this.context.Posts.FindAsync(id);

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

        public async Task<PostEditFormModel> GetPostByIdAsync(int id)
        {
            Post post = await this.context.Posts.FindAsync(id);

            return new PostEditFormModel()
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
            Post? post = await this.context.Posts.FindAsync(postId);

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
                .Skip(3 * (counter - 1 == -1 ? 0 : counter - 1))
                .Take(3)
                .ToListAsync();

            List<PostViewModel> posts = await this.context.Posts
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
                //.Skip(3 * (counter - 1 == -1 ? 0 : counter - 1))
                //.Take(3)
                .ToListAsync();

            List<PostViewModel> mostRecentlyLikedPosts = new List<PostViewModel>();

            if (posts.Count != 0)
            {
                foreach (var post in allLikedPosts)
                {
                    mostRecentlyLikedPosts.Add(posts.First(p => p.Id == post));
                }
            }

            return mostRecentlyLikedPosts;
        }

        public async Task<List<string>> AllAdminIdsAsync()
        {
            List<ApplicationUser> allAdmins
                = (List<ApplicationUser>)await this.userManager.GetUsersInRoleAsync("Administrator");

            List<ApplicationUser> allSuperAdmins
                = (List<ApplicationUser>)await this.userManager.GetUsersInRoleAsync("SuperAdministrator");

            List<string> adminsIds = allAdmins.Select(a => a.Id).ToList();
            adminsIds.AddRange(allSuperAdmins.Select(sa => sa.Id).ToList());

            return adminsIds;
        }

        public async Task<List<ProfileViewModel>> GetProfilesAsync(string? search, int counter)
        {
            List<ProfileViewModel> profiles = new List<ProfileViewModel>();
            List<string> adminIds = await AllAdminIdsAsync();

            if (string.IsNullOrEmpty(search))
            {
                profiles = await this.context.Users
                    .Where(u => !adminIds.Contains(u.Id))
                        .Select(u => new ProfileViewModel()
                        {
                            Id = u.Id,
                            Username = u.UserName!,
                            FullName = $"{u.FirstName} {u.LastName}",
                            TotalPosts = u.Posts.Count,
                            CountryName = u.Country.Name
                        })
                    .Skip(10 * (counter - 1 == -1 ? 0 : counter - 1))
                    .Take(10)
                    .ToListAsync();
            }
            else
            {
                profiles = await this.context.Users
                    .Where(u => (u.UserName!.Contains(search) ||
                        (u.FirstName + " " + u.LastName).Contains(search))
                        && !adminIds.Contains(u.Id))
                        .Select(u => new ProfileViewModel()
                        {
                            Id = u.Id,
                            Username = u.UserName!,
                            FullName = $"{u.FirstName} {u.LastName}",
                            TotalPosts = u.Posts.Count,
                            CountryName = u.Country.Name
                        })
                    .Skip(10 * (counter - 1 == -1 ? 0 : counter - 1))
                    .Take(10)
                    .ToListAsync();
            }

            return profiles;
        }

        public async Task<List<PostViewModel>> GetPostsByProfileAsync(int counter, string username)
        {
            return await this.context.Posts
                .Where(p => p.User.UserName == username)
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

        public async Task ReportPostAsync(int id)
        {
            ReportPost? reportPost = await this.context.ReportPosts
                .Where(rp => rp.PostId == id)
                .FirstOrDefaultAsync();

            if (reportPost == null)
            {
                reportPost = new ReportPost()
                {
                    PostId = id,
                    ReportsCount = 1
                };

                await this.context.ReportPosts.AddAsync(reportPost);
            }
            else
            {
                reportPost.ReportsCount++;
            }

            await this.context.SaveChangesAsync();
        }

        public async Task<PostViewModel> GetReportPostAsync(int id)
        {
            PostViewModel post = await this.context.Posts
                .Where(p => p.Id == id)
                .Select(p => new PostViewModel()
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
                .FirstAsync();

            return post;
        }

        public async Task DismissReportedPostAsync(int postId)
        {
            ReportPost postToDismiss = await this.context.ReportPosts
                .Where(p => p.PostId == postId)
                .FirstAsync();

            this.context.ReportPosts.Remove(postToDismiss);
            await this.context.SaveChangesAsync();
        }

        public async Task IncreaseDeletedReportedPostsCountAsync()
        {
            var statistic = await this.context.Statistics
                .FirstAsync();

            statistic.ReportedPostsDeletedCount++;

            await this.context.SaveChangesAsync();
        }
    }
}
