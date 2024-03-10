using SocialMedia.Areas.Admin.ViewModels.AdminChat;

namespace SocialMedia.Areas.Admin.Services.Interfaces
{
    public interface IAdminChatService
    {
        Task SaveMessageToDatabaseAsync(string user, string message);

        Task<List<AdminMessageViewModel>> LoadMessagesFromDatabaseAsync();
    }
}
