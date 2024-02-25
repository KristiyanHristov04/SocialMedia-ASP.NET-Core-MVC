using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.ViewModels.AdminArea.Report
{
    public class ReportViewModel
    {
        public string UserFullName { get; set; } = null!;
        public string UserUsername { get; set; } = null!;
        public int TotalReports { get; set; }
        public int PostId { get; set; }
        public string PostPath { get; set; } = null!;
    }
}
