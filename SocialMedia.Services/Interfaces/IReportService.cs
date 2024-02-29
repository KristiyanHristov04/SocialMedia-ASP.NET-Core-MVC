using SocialMedia.ViewModels.AdminArea.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services.Interfaces
{
    public interface IReportService
    {
        Task<AllViewModel> GetReportsAsync
            (string filter, int reportsPerPage, int currentPage);
    }
}
