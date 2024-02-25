using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
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

        public async Task<List<ReportViewModel>> GetReportsAsync()
        {
            return await this.context.ReportPosts.Select(rp => new ReportViewModel()
            {
                UserFullName = rp.Post.User.FirstName + " " + rp.Post.User.LastName,
                UserUsername = rp.Post.User.UserName!,
                TotalReports = rp.ReportsCount,
                PostId = rp.PostId,
                PostPath = rp.Post.Path
            })
                .ToListAsync();
        }
    }
}
