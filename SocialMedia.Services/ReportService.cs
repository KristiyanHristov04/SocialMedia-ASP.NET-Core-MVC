using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Data.Models;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.AdminArea.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
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
            int currentPage = 1,
            int reportsPerPage = 2
            )
        {
            IQueryable<ReportPost> reportsQuery = this.context.ReportPosts.AsQueryable();

            if (filter != null)
            {
                if (filter == "ascending")
                {
                    reportsQuery = reportsQuery.OrderBy(r => r.ReportsCount);
                }
                else
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
