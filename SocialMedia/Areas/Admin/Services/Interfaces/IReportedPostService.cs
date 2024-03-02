using SocialMedia.Areas.Admin.ViewModels.Post;

namespace SocialMedia.Areas.Admin.Services.Interfaces
{
    public interface IReportedPostService
    {
        Task<bool> ReportedPostExistsByIdAsync(int postId);
        Task<PreviewViewModel> GetReportedPostPreviewInformationAsync(int postId);
    }
}
