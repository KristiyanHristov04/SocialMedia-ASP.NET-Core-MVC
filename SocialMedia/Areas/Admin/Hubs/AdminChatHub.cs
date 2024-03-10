using Microsoft.AspNetCore.SignalR;
using SocialMedia.Areas.Admin.Services.Interfaces;

namespace SocialMedia.Areas.Admin.Hubs
{
    public class AdminChatHub : Hub
    {
        private readonly IAdminChatService adminChatService;
        public AdminChatHub(IAdminChatService adminChatService)
        {
            this.adminChatService = adminChatService;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            await adminChatService.SaveMessageToDatabaseAsync(user, message);
        }
    }
}
