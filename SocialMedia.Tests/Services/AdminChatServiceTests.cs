using Microsoft.EntityFrameworkCore;
using SocialMedia.Areas.Admin.Services;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.AdminChat;
using SocialMedia.Data.Models;

namespace SocialMedia.Tests.Services
{
    public class AdminChatServiceTests : BaseTest
    {
        private IAdminChatService adminChatService;
        public AdminChatServiceTests()
        {
            this.adminChatService = new AdminChatService(context);
        }

        [Fact]
        public async Task SaveMessageToDatabaseAsync_ShouldSaveMessage()
        {
            await this.adminChatService.SaveMessageToDatabaseAsync("John", "Hello");
            await this.adminChatService.SaveMessageToDatabaseAsync("John", "How are you?");

            int messagesCount = await this.context.AdminMessages.CountAsync();
            AdminMessage firstMessage = await this.context.AdminMessages.FirstAsync();

            Assert.Equal(2, messagesCount);
            Assert.Equal("John", firstMessage.Username);
            Assert.Equal("Hello", firstMessage.Message);
        }

        [Fact]
        public async Task LoadMessagesFromDatabaseAsync_ShouldReturnMessages()
        {
            await this.adminChatService.SaveMessageToDatabaseAsync("John", "Hello");
            await this.adminChatService.SaveMessageToDatabaseAsync("John", "How are you?");

            var messages = await this.adminChatService.LoadMessagesFromDatabaseAsync();
            var firstMessage = messages.First();

            Assert.NotEmpty(messages);
            Assert.IsType<AdminMessageViewModel>(firstMessage);
            Assert.Equal("John", firstMessage.Username);
            Assert.Equal("Hello", firstMessage.Message);
        }
    }
}
