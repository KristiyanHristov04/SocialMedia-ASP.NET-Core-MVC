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
        public async Task<IActionResult> All(AllViewModel allModel)
        {
            AllViewModel model
                = await this.reportService.GetReportsAsync
                (allModel.Filter,
                allModel.CurrentPage,
                AllViewModel.ReportsPerPage);

            allModel.TotalReports = model.TotalReports;
            allModel.Reports = model.Reports;

            return View(allModel);
        }
    }
}
