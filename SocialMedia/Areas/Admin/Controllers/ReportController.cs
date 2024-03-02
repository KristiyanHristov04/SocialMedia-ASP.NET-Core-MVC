using Microsoft.AspNetCore.Mvc;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Report;

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
            if (allModel.CurrentPage <= 0)
            {
                allModel.CurrentPage = 1;
            }

            AllViewModel model
                = await this.reportService.GetReportsAsync
                (allModel.Filter,
                AllViewModel.ReportsPerPage,
                allModel.CurrentPage
                );

            allModel.TotalReports = model.TotalReports;
            allModel.Reports = model.Reports;

            return View(allModel);
        }
    }
}
