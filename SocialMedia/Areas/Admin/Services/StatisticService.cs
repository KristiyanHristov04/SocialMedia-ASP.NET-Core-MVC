using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Home;
using SocialMedia.Data;

namespace SocialMedia.Areas.Admin.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        public StatisticService(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.roleManager = roleManager;
        }

        public async Task<StatisticsViewModel> GetStatisticsAsync()
        {
            var role = await this.roleManager.FindByNameAsync("Administrator");
            int totalAdmins = this.context.UserRoles.Where(ur => ur.RoleId == role!.Id).Count();

            int registeredUserslast7Days = await this.context.Users
                .Where(u => u.RegistrationDate.AddDays(7) >= DateTime.Now)
                .CountAsync();

            return new StatisticsViewModel()
            {
                ReportedPostsDeletedCount = await this.context.Statistics
                .Select(s => s.ReportedPostsDeletedCount).FirstAsync(),
                AllTimeUsersCount = await this.context.Statistics
                .Select(s => s.AllTimeUsersCount).FirstAsync(),
                TotalAdminsCount = totalAdmins,
                RegisteredUsersLast7DaysCount = registeredUserslast7Days
            };
        }
    }
}
