using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Home;
using SocialMedia.Data;
using SocialMedia.Data.Models;

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
            var adminRole = await this.roleManager.FindByNameAsync("Administrator");
            var superAdminRole = await this.roleManager.FindByNameAsync("SuperAdministrator");
            int totalAdmins = this.context.UserRoles.Where(ur => ur.RoleId == adminRole!.Id).Count();
            totalAdmins += this.context.UserRoles.Where(ur => ur.RoleId == superAdminRole!.Id).Count();

            int registeredUserslast7Days = await this.context.Users
                .Where(u => u.RegistrationDate.AddDays(7) >= DateTime.Now
                && u.Email != "admin@socialmedia.com")
                .CountAsync();

            var stats = await this.context.Statistics.FindAsync(1);

            return new StatisticsViewModel()
            {
                ReportedPostsDeletedCount = stats!.ReportedPostsDeletedCount,
                AllTimeUsersCount = stats.AllTimeUsersCount,
                TotalAdminsCount = totalAdmins,
                RegisteredUsersLast7DaysCount = registeredUserslast7Days
            };
        }
    }
}
