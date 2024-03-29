using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using SocialMedia.Areas.Admin.Services;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Tests.Services
{
    public class StatisticServiceTests : BaseTest
    {
        private IStatisticService statisticService;
        private Mock<RoleManager<IdentityRole>> roleManagerMock;
        public StatisticServiceTests()
        {
            this.roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                    Mock.Of<IRoleStore<IdentityRole>>(),
                    It.IsAny<IEnumerable<IRoleValidator<IdentityRole>>>(),
                    Mock.Of<ILookupNormalizer>(),
                    Mock.Of<IdentityErrorDescriber>(),
                    Mock.Of<ILogger<RoleManager<IdentityRole>>>()
                );

            this.roleManagerMock.Setup(rm => rm.FindByNameAsync("Administrator"))
                .ReturnsAsync(new IdentityRole() { Id = Guid.NewGuid().ToString() });

            this.roleManagerMock.Setup(rm => rm.FindByNameAsync("SuperAdministrator"))
                .ReturnsAsync(new IdentityRole() { Id = Guid.NewGuid().ToString() });

            this.statisticService = new StatisticService(context, roleManagerMock.Object);
        }

        [Fact]
        public async Task GetStatisticsAsync_ShouldReturnStatsticsAboutApplication()
        {
            var result = await this.statisticService.GetStatisticsAsync();

            Assert.NotNull(result);
            Assert.IsType<StatisticsViewModel>(result);
            Assert.Equal(0, result.TotalAdminsCount);
            Assert.Equal(0, result.AllTimeUsersCount);
            Assert.Equal(2, result.RegisteredUsersLast7DaysCount);
        }
    }
}
