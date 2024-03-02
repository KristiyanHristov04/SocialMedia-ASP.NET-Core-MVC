using SocialMedia.Areas.Admin.ViewModels.Report;

namespace SocialMedia.Areas.Admin.Services.Interfaces
{
    public interface IReportService
    {
        Task<AllViewModel> GetReportsAsync
            (string filter, int reportsPerPage, int currentPage);
    }
}
