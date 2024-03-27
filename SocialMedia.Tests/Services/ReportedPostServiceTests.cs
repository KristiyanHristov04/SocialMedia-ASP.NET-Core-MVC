using SocialMedia.Areas.Admin.Services;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Post;

namespace SocialMedia.Tests.Services
{
    public class ReportedPostServiceTests : BaseTest
    {
        private IReportedPostService reportedPostService;
        public ReportedPostServiceTests()
        {
            this.reportedPostService = new ReportedPostService(context);
        }

        [Fact]
        public async Task ReportedPostExistsByIdAsync_ShouldReturnTrueIfPostExistsInReportedPosts()
        {
            bool result = await this.reportedPostService
                .ReportedPostExistsByIdAsync(Post02.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task ReportedPostExistsByIdAsync_ShouldReturnFalseIfPostDoesntExistInReportedPosts()
        {
            bool result = await this.reportedPostService
                .ReportedPostExistsByIdAsync(Post01.Id);

            Assert.False(result);
        }

        [Fact]
        public async Task GetReportedPostPreviewInformationAsync_ShouldReturnPostPreview()
        {
            var result = await this.reportedPostService
                .GetReportedPostPreviewInformationAsync(Post02.Id);

            Assert.Equal(2, result.PostId);
            Assert.Equal("Jane", result.Username);
            Assert.IsType<PreviewViewModel>(result);
        }
    }
}
