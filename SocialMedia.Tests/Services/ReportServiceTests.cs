using SocialMedia.Areas.Admin.Services;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Report;
using SocialMedia.Data.Models;
using System.Collections;

namespace SocialMedia.Tests.Services
{
    public class ReportServiceTests : BaseTest
    {
        private IReportService reportService;
        private ReportPost reportPostOne;
        private ReportPost reportPostTwo;
        private ReportPost reportPostThree;
        public ReportServiceTests()
        {
            this.reportService = new ReportService(context);

            this.reportPostOne = new ReportPost()
            {
                Id = 2,
                Post = Post01,
                ReportsCount = 7
            };

            this.reportPostTwo = new ReportPost()
            {
                Id = 3,
                Post = new Post()
                {
                    Id = 3,
                    Text = "Post3",
                    Path = "somepath",
                    User = User01,
                    Date = DateTime.Now
                },
                ReportsCount = 8
            };

            this.reportPostThree = new ReportPost()
            {
                Id = 4,
                Post = new Post()
                {
                    Id = 4,
                    Text = "Post4",
                    Path = "somepath",
                    User = User02,
                    Date = DateTime.Now
                },
                ReportsCount = 2
            };

            this.context.ReportPosts.AddRange(new[] { this.reportPostOne, this.reportPostTwo, this.reportPostThree });
            this.context.SaveChanges();
        }

        [Fact]
        public async Task GetReportsAsync_ShouldReturnCorrectDataWithoutFilter()
        {
            var reportedPostsInformation 
                = await this.reportService.GetReportsAsync(null, 3, 1);

            var reportedPost = reportedPostsInformation.Reports.First();

            Assert.Equal(Post02.Id, reportedPost.PostId);
            Assert.Equal(Post02.Path, reportedPost.PostPath);
            Assert.Equal(Post02.User.FirstName + " " + Post02.User.LastName, reportedPost.UserFullName);
            Assert.Equal(Post02.User.UserName, reportedPost.UserUsername);

            Assert.NotEmpty(reportedPostsInformation.Reports);
            Assert.Equal(4, reportedPostsInformation.TotalReports);
            Assert.IsType<AllViewModel>(reportedPostsInformation);
            Assert.IsType<int>(reportedPostsInformation.TotalReports);
            Assert.IsType<List<ReportViewModel>>(reportedPostsInformation.Reports);
        }

        [Fact]
        public async Task GetReportsAsync_ShouldReturnCorrectDataWithAscendingFilter()
        {
            var reportedPostsInformation
               = await this.reportService.GetReportsAsync("ascending", 3, 1);

            var reportedPost = reportedPostsInformation.Reports.First();

            Assert.Equal(Post02.Id, reportedPost.PostId);
            Assert.Equal(Post02.Path, reportedPost.PostPath);
            Assert.Equal(Post02.User.FirstName + " " + Post02.User.LastName, reportedPost.UserFullName);
            Assert.Equal(Post02.User.UserName, reportedPost.UserUsername);

            Assert.NotEmpty(reportedPostsInformation.Reports);
            Assert.Equal(4, reportedPostsInformation.TotalReports);
            Assert.IsType<AllViewModel>(reportedPostsInformation);
            Assert.IsType<int>(reportedPostsInformation.TotalReports);
            Assert.IsType<List<ReportViewModel>>(reportedPostsInformation.Reports);
        }

        [Fact]
        public async Task GetReportsAsync_ShouldReturnCorrectDataWithDescendingFilter()
        {
            var reportedPostsInformation
               = await this.reportService.GetReportsAsync("descending", 3, 1);

            var reportedPost = reportedPostsInformation.Reports.First();

            Assert.Equal(reportPostTwo.PostId, reportedPost.PostId);
            Assert.Equal(reportPostTwo.Post.Path, reportedPost.PostPath);
            Assert.Equal(reportPostTwo.Post.User.FirstName + " " + reportPostTwo.Post.User.LastName, reportedPost.UserFullName);
            Assert.Equal(reportPostTwo.Post.User.UserName, reportedPost.UserUsername);

            Assert.NotEmpty(reportedPostsInformation.Reports);
            Assert.Equal(4, reportedPostsInformation.TotalReports);
            Assert.IsType<AllViewModel>(reportedPostsInformation);
            Assert.IsType<int>(reportedPostsInformation.TotalReports);
            Assert.IsType<List<ReportViewModel>>(reportedPostsInformation.Reports);
        }
    }
}
