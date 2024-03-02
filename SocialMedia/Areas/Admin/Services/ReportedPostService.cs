using Microsoft.EntityFrameworkCore;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Post;
using SocialMedia.Data;
using SocialMedia.Data.Models;

namespace SocialMedia.Areas.Admin.Services
{
    public class ReportedPostService : IReportedPostService
    {
        private readonly ApplicationDbContext context;
        public ReportedPostService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> ReportedPostExistsByIdAsync(int postId)
        {
            return await this.context.ReportPosts.AnyAsync(rp => rp.PostId == postId);
        }

        public async Task<PreviewViewModel> GetReportedPostPreviewInformationAsync(int postId)
        {
            return new PreviewViewModel()
            {
                PostId = postId,
                Username = await this.context.Posts
                .Where(p => p.Id == postId)
                .Select(p => p.User.UserName!)
                .FirstAsync()
            };
        }
    }
}
