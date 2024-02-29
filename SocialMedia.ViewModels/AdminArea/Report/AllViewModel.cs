using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.ViewModels.AdminArea.Report
{
    public class AllViewModel
    {
        public List<ReportViewModel> Reports { get; set; } = null!;
        public int TotalReports { get; set; }
        public const int ReportsPerPage = 4;
        public int CurrentPage { get; set; } = 1;
        public string Filter { get; set; } = null!;

    }
}
