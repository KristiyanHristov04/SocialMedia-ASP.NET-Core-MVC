using SocialMedia.Data.Models;
using SocialMedia.Data;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.AdminChat;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Areas.Admin.Services
{
    public class AdminChatService : IAdminChatService
    {
        private readonly ApplicationDbContext context;
        public AdminChatService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<AdminMessageViewModel>> LoadMessagesFromDatabaseAsync()
        {
            return await this.context.AdminMessages
                .Select(am => new AdminMessageViewModel()
                {
                    Username = am.Username,
                    Message = am.Message,
                    SentDate = am.SentDate.ToString("dd.MM.yyyy"),
                })
            .ToListAsync();
        }

        public async Task SaveMessageToDatabaseAsync(string user, string message)
        {
            AdminMessage adminMessage = new AdminMessage()
            {
                Username = user,
                Message = message,
                SentDate = DateTime.Now
            };

            await this.context.AdminMessages.AddAsync(adminMessage);
            await this.context.SaveChangesAsync();
        }
    }
}
