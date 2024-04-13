using Microsoft.EntityFrameworkCore;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Announcement;
using SocialMedia.Data;
using SocialMedia.Data.Models;

namespace SocialMedia.Areas.Admin.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly ApplicationDbContext context;
        public AnnouncementService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAnnouncementAsync(string userId, AnnouncementCreateFormModel model)
        {
            Announcement announcement = new Announcement()
            {
                Title = model.Title,
                Description = model.Description,
                PublishDate = DateTime.Now,
                UserId = userId
            };

            await context.Announcements.AddAsync(announcement);
            await context.SaveChangesAsync();
        }

        public async Task<List<AnnouncementViewModel>> GetAnnouncementsAsync()
        {
            return await this.context.Announcements
                .OrderByDescending(a => a.Id)
                .Select(a => new AnnouncementViewModel()
                {
                    Title = a.Title,
                    Description = a.Description,
                    PublishDate = a.PublishDate.ToString("dd.MM.yyyy"),
                    UserUsername = a.User == null ? null : a.User.UserName
                })
                .ToListAsync();
        }
    }
}
