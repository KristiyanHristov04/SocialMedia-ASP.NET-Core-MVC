using SocialMedia.Areas.Admin.ViewModels.User;

namespace SocialMedia.Areas.Admin.Services.Interfaces
{
    public interface IUserService
    {
        Task<AllViewModel> GetUsersAsync(string filter, int currentPage);
        Task<List<string>> GetRolesByUserByIdAsync(string userId);
    }
}
