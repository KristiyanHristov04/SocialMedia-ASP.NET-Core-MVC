using Microsoft.EntityFrameworkCore;
using SocialMedia.Areas.Admin.Services;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Announcement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Tests.Services
{
    public class AnnouncementServiceTests : BaseTest
    {
        private readonly IAnnouncementService announcementService;
        public AnnouncementServiceTests()
        {
            this.announcementService = new AnnouncementService(context);
        }

        [Fact]
        public async Task CreateAnnouncementAsync_ShouldCreateNewAnnouncement()
        {
            AnnouncementFormModel model = new AnnouncementFormModel()
            {
                Title = "Title",
                Description = "Description"
            };

            await this.announcementService.CreateAnnouncementAsync(User01.Id, model);

            int totalAnnouncements = await this.context.Announcements.CountAsync();
            Assert.Equal(2, totalAnnouncements);
        }

        [Fact]
        public async Task GetAnnouncementsAsync_ShouldReturnAllAnnouncements()
        {
            var announcements = await this.announcementService.GetAnnouncementsAsync();

            var firstAnnouncements = announcements.First();

            Assert.NotEmpty(announcements);
            Assert.Single(announcements);
            Assert.Equal(Announcement01.Title, firstAnnouncements.Title);
            Assert.Equal(Announcement01.Description, firstAnnouncements.Description);
        }

        [Fact]
        public async Task GetAnnouncementByIdAsync_ShouldReturnCorrectAnnouncement()
        {
            var announcement = await this.announcementService.GetAnnouncementByIdAsync(1);

            Assert.NotNull(announcement);
            Assert.Equal(Announcement01.Title, announcement.Title);
            Assert.Equal(Announcement01.Description, announcement.Description);
        }

        [Fact]
        public async Task GetAnnouncementByIdAsync_ShouldThrowExceptionIfIdIsNotFound()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                var announcement = await this.announcementService.GetAnnouncementByIdAsync(10);
            });
        }

        [Fact]
        public async Task CheckIfAnnouncementExistsById_ShouldTrueIfIdExists()
        {
            bool result = await this.announcementService.CheckIfAnnouncementExistsById(1);

            Assert.True(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(-1)]
        [InlineData(100)]
        public async Task CheckIfAnnouncementExistsById_ShouldFalseIfIdExists(int id)
        {
            bool result = await this.announcementService.CheckIfAnnouncementExistsById(id);

            Assert.False(result);
        }

        [Fact]
        public async Task EditAnnouncementAsync_ShouldEditTheActualAnnouncement()
        {
            var newAnnouncement = new AnnouncementFormModel()
            {
                Title = "EditedTitle",
                Description = "EditedTitle"
            };

            await this.announcementService
                .EditAnnouncementAsync(Announcement01.Id, newAnnouncement);

            var announcement = this.context.Announcements
                .Where(a => a.Id == Announcement01.Id)
                .First();

            Assert.Equal(newAnnouncement.Title, announcement.Title);
            Assert.Equal(newAnnouncement.Description, announcement.Description);
        }

        [Fact]
        public async Task DeleteAnnouncementAsync_ShouldRemoveAnnouncementFromDatabase()
        {
            await this.announcementService.DeleteAnnouncementAsync(1, null);

            var totalAnnouncements = await this.context.Announcements.CountAsync();

            Assert.Equal(0, totalAnnouncements);
        }
    }
}
