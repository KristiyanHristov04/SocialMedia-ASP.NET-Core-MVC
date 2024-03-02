using SocialMedia.Areas.Admin.ViewModels.Home;

namespace SocialMedia.Areas.Admin.Services.Interfaces
{
    public interface IStatisticService
    {
        Task<StatisticsViewModel> GetStatisticsAsync();
    }
}
