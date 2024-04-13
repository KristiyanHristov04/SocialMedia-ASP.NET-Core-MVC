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

        public async Task CreateAnnouncementAsync(string userId, AnnouncementFormModel model)
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
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    PublishDate = a.PublishDate.ToString("dd.MM.yyyy"),
                    UserUsername = a.User == null ? null : a.User.UserName
                })
                .ToListAsync();
        }


        public Task<AnnouncementFormModel> GetAnnouncementByIdAsync(int id)
        {
            return this.context.Announcements
                .Where(a => a.Id == id)
                .Select(a => new AnnouncementFormModel()
            {
                Description = a.Description,
                Title = a.Title
            })
            .FirstAsync();
        }

        public async Task<bool> CheckIfAnnouncementExistsById(int id)
        {
            return await this.context.Announcements.AnyAsync(a => a.Id == id);
        }

        public async Task EditAnnouncementAsync(int id, AnnouncementFormModel model)
        {
            Announcement announcement = await this.context.Announcements
                .FirstAsync(a => a.Id == id);

            announcement.Title = model.Title;
            announcement.Description = model.Description;

            await this.context.SaveChangesAsync();
        }
    }
}
