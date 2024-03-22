using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data.Models;
using SocialMedia.Services;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SocialMedia.Tests.Services
{
    public class PostServiceTests : BaseTest
    {
        private readonly IPostService postService;
        public PostServiceTests()
        {
            this.postService = new PostService(context, null, null);
        }

        [Fact]
        public async Task GetPostByIdAsync_ShouldReturnCorrectPostFormModel()
        {
            var post = await this.postService.GetPostByIdAsync(Post01.Id);

            Assert.Equal("Post1", post.Text);
        }

        [Fact]
        public async Task ValidateIfPostExistsAsync_ShouldReturnTrueIfExists()
        {
            bool result = await this.postService.ValidateIfPostExistsAsync(Post01.Id);

            Assert.True(result);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(50)]
        public async Task ValidateIfPostExistsAsync_ShouldReturnFalseIfDoesntExist(int id)
        {
            bool result = await this.postService.ValidateIfPostExistsAsync(id);

            Assert.False(result);
        }

        [Fact]
        public async Task ValidatePostUserAsync_ShouldReturnTrueIfUserIsCreatorOfThePost()
        {
            bool result = await this.postService.ValidatePostUserAsync(User01.Id, Post01.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task ValidatePostUserAsync_ShouldReturnFalseIfUserIsNotCreatorOfThePost()
        {
            bool result = await this.postService.ValidatePostUserAsync(User02.Id, Post01.Id);

            Assert.False(result);
        }

        [Fact]
        public async Task EditPostAsync_ShouldEditPostSuccessfully()
        {
            PostEditFormModel postModel = new PostEditFormModel()
            {
                Text = "Edited post"
            };

            await this.postService.EditPostAsync(1, postModel);

            var post = await this.context.Posts.FindAsync(1);

            Assert.Equal("Edited post", post!.Text);
        }

        [Fact]
        public async Task CheckIfPostByUserIsLikedAsync_ShouldReturnTrue()
        {
            bool result = await this.postService.CheckIfPostByUserIsLikedAsync(Post01.Id, User01.Id);
            Assert.True(result);
        }

        [Fact]
        public async Task CheckIfPostByUserIsLikedAsync_ShouldReturnFalse()
        {
            bool result = await this.postService.CheckIfPostByUserIsLikedAsync(Post02.Id, User01.Id);
            Assert.False(result);
        }

        [Fact]
        public async Task LikeDislikePostAsync_ShouldRemovePostFromLikedPostsForUser()
        {
            await this.postService.LikeDislikePostAsync(Post01.Id, User01.Id);

            Assert.Equal(0, User01.LikedPosts.Count);
        }

        [Fact]
        public async Task LikeDislikePostAsync_ShouldAddPostToLikedPostsForUser()
        {
            await this.postService.LikeDislikePostAsync(Post01.Id, User02.Id);

            Assert.Equal(2, User02.LikedPosts.Count);
        }

        [Fact]
        public async Task GetMyPostsAsync_ShouldReturnCorrectCount()
        {
            var myPosts = await this.postService.GetMyPostsAsync(1, User01.Id);

            int totalPosts = myPosts.Count();

            var firstPost = myPosts.First();

            Assert.Equal(1, totalPosts);    
            Assert.Equal(Post01.Id, firstPost.Id);    
            Assert.Equal(Post01.Text, firstPost.Text);    
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task GetPostsAsync_ShouldLoadFirstThreePosts(int counter)
        {
            var posts = await this.postService.GetPostsAsync(counter);

            Assert.Equal(2, posts.Count());
        }

        [Fact]
        public async Task GetMyLikedPostsAsync_ShouldReturnLikedPostsByUser()
        {
            var likedPostsUser01 = await this.postService.GetMyLikedPostsAsync(1, User01.Id);
            var likedPostsUser02 = await this.postService.GetMyLikedPostsAsync(1, User02.Id);

            var firstLikedPostUser01 = likedPostsUser01.First();
            var firstLikedPostUser02 = likedPostsUser02.First();

            Assert.Single(likedPostsUser01);
            Assert.Single(likedPostsUser02);
            Assert.NotSame(firstLikedPostUser01, firstLikedPostUser02);
        }

        [Fact]
        public async Task GetPostsByProfileAsync_ShouldReturnPostsByGivenUser()
        {
            var posts = await this.postService.GetPostsByProfileAsync(1, User01.UserName!);

            int postsCount = posts.Count();

            Assert.Equal(1, postsCount);
        }

        [Fact]
        public async Task ReportPostAsync_ShouldAddNewReportedPost()
        {
            await this.postService.ReportPostAsync(Post01.Id);

            var newlyReportedPost = 
                await this.context.ReportPosts
                .Where(rp => rp.PostId == Post01.Id)
                .FirstAsync();

            Assert.Equal(1, newlyReportedPost.ReportsCount);
            Assert.Equal(Post01.Text, newlyReportedPost.Post.Text);
            Assert.Equal(Post01.Date, newlyReportedPost.Post.Date);
        }

        [Fact]
        public async Task ReportPostAsync_ShouldIncreaseReportsCountForGivenPost()
        {
            await this.postService.ReportPostAsync(Post02.Id);

            int reportsCount = await this.context.ReportPosts
                .Where(rp => rp.PostId == Post02.Id)
                .Select(rp => rp.ReportsCount)
                .FirstAsync();

            Assert.Equal(2, reportsCount);
        }

        [Fact]
        public async Task GetReportPostAsync_ShouldReturnReportedPost()
        {
            var reportedPost = await this.postService.GetReportPostAsync(Post02.Id);

            Assert.Equal(User02.Id, reportedPost.UserId);
            Assert.NotNull(reportedPost);
        }

        [Fact]
        public async Task DismissReportedPostAsync_RemoveReportedPostFromDatabase()
        {
            await this.postService.DismissReportedPostAsync(Post02.Id);

            var post = this.context.ReportPosts
                .Where(rp => rp.PostId == Post02.Id)
                .FirstOrDefault();

            int reportedPostsCount = await this.context.ReportPosts.CountAsync();

            Assert.Null(post);
            Assert.Equal(0, reportedPostsCount);
        }

        [Fact]
        public async Task IncreaseDeletedReportedPostsCountAsync_ShouldIncreaseWhenReportedPostIsDeleted()
        {
            await this.postService.IncreaseDeletedReportedPostsCountAsync();

            int count = await this.context.Statistics
                .Select(s => s.ReportedPostsDeletedCount)
                .FirstAsync();

            Assert.Equal(1, count);
        }
    }
}
