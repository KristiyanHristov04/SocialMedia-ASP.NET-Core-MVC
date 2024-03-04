using SocialMedia.Areas.Admin.ViewModels.User;

namespace SocialMedia.Areas.Admin.Services.Interfaces
{
    public interface IUserService
    {
        Task<AllViewModel> GetUsersAsync(string filter, int currentPage);
        Task<string?> GetRoleByUserId(string userId);
        Task<UserViewModel> GetUserAsync(string userId);
        Task<bool> CheckIfUserEligibleForPromoteAsync(string id);

    }
}
