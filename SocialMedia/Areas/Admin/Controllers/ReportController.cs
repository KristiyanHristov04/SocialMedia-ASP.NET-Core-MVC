using Microsoft.AspNetCore.Mvc;
using SocialMedia.Services;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.AdminArea.Report;

namespace SocialMedia.Areas.Admin.Controllers
{
    public class ReportController : AdminController
    {
        private readonly IReportService reportService;
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            List<ReportViewModel> reports
                = await this.reportService.GetReportsAsync();

            return View(reports);
        }
    }
}
