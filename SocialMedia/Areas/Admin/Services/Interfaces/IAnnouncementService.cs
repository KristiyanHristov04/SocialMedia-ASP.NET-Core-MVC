using SocialMedia.Areas.Admin.ViewModels.Announcement;

namespace SocialMedia.Areas.Admin.Services.Interfaces
{
    public interface IAnnouncementService
    {
        public Task CreateAnnouncementAsync(string userId, AnnouncementFormModel model);
        public Task<List<AnnouncementViewModel>> GetAnnouncementsAsync();
        public Task<AnnouncementFormModel> GetAnnouncementByIdAsync(int id);
        public Task<bool> CheckIfAnnouncementExistsById(int id);
        Task EditAnnouncementAsync(int id, AnnouncementFormModel model);
    }
}
