using SocialMedia.Areas.Admin.ViewModels.Announcement;

namespace SocialMedia.Areas.Admin.Services.Interfaces
{
    public interface IAnnouncementService
    {
        public Task CreateAnnouncementAsync(string userId, AnnouncementCreateFormModel model);
        public Task<List<AnnouncementViewModel>> GetAnnouncementsAsync();
    }
}
