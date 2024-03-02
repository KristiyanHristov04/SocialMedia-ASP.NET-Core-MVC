using Microsoft.EntityFrameworkCore;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Report;
using SocialMedia.Data;
using SocialMedia.Data.Models;

namespace SocialMedia.Areas.Admin.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext context;
        public ReportService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AllViewModel> GetReportsAsync(
            string filter,
            int reportsPerPage,
            int currentPage
            )
        {
            IQueryable<ReportPost> reportsQuery = this.context.ReportPosts.AsQueryable();

            if (filter != null)
            {
                if (filter == "ascending")
                {
                    reportsQuery = reportsQuery.OrderBy(r => r.ReportsCount);
                }
                else if (filter == "descending")
                {
                    reportsQuery = reportsQuery.OrderByDescending(r => r.ReportsCount);
                }
            }

            List<ReportViewModel> reports = await reportsQuery
                .Skip((currentPage - 1) * reportsPerPage)
                .Take(reportsPerPage)
                .Select(r => new ReportViewModel()
                {
                    UserFullName = r.Post.User.FirstName + " " + r.Post.User.LastName,
                    UserUsername = r.Post.User.UserName!,
                    TotalReports = r.ReportsCount,
                    PostId = r.PostId,
                    PostPath = r.Post.Path
                })
                .ToListAsync();

            int totalReports = reportsQuery.Count();

            return new AllViewModel()
            {
                Reports = reports,
                TotalReports = totalReports
            };
        }
    }
}
