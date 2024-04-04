namespace SocialMedia.Areas.Admin.ViewModels.Report
{
    public class AllViewModel
    {
        public List<ReportViewModel> Reports { get; set; } = null!;
        public int TotalReports { get; set; }
        public const int ReportsPerPage = 5;
        public int CurrentPage { get; set; } = 1;
        public string Filter { get; set; } = null!;

    }
}
