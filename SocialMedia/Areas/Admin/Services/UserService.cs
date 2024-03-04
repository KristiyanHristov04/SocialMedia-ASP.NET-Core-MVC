using Microsoft.EntityFrameworkCore;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.User;
using SocialMedia.Data;
using SocialMedia.Data.Models;

namespace SocialMedia.Areas.Admin.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<string>> GetRolesByUserByIdAsync(string id)
        {
            List<string> rolesIds = await this.context.UserRoles
                .Where(ur => ur.UserId == id)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            List<string?> roles = await this.context.Roles
                .Where(r => rolesIds.Contains(r.Id))
                .Select(r => r.Name)
                .ToListAsync();

            return roles!;
        }

        public async Task<UserViewModel> GetUserAsync(string userId)
        { 
            UserViewModel user = await this.context.Users.Where(u => u.Id == userId)
                .Select(u => new UserViewModel()
                {
                    UserId = u.Id,
                    UserUsername = u.UserName!,
                    UserEmail = u.Email!,
                    UserFullName = u.FirstName + " " + u.LastName,
                    JoinedDate = u.RegistrationDate.ToString("dd.MM.yyyy")
                })
                .FirstAsync();

            user.UserRoles = await GetRolesByUserByIdAsync(userId);

            return user;
        }

        public async Task<AllViewModel> GetUsersAsync(string filter, int currentPage)
        {
            IQueryable<ApplicationUser> usersQuery = this.context.Users.AsQueryable();

            if (filter != null)
            {
                if (filter == "newest")
                {
                    usersQuery = usersQuery.OrderByDescending(u => u.RegistrationDate);
                }
                else if (filter == "oldest")
                {
                    usersQuery = usersQuery.OrderBy(u => u.RegistrationDate);
                }
            }

            List<UserViewModel> users = await usersQuery
                .Skip((currentPage - 1) * AllViewModel.UsersPerPage)
                .Take(AllViewModel.UsersPerPage)
                .Select(u => new UserViewModel()
                {
                    UserId = u.Id,
                    UserUsername = u.UserName!,
                    UserEmail = u.Email!,
                    UserFullName = u.FirstName + " " + u.LastName,
                    JoinedDate = u.RegistrationDate.ToString("dd.MM.yyyy")
                })
                .ToListAsync();

            foreach (var user in users)
            {
                user.UserRoles = await GetRolesByUserByIdAsync(user.UserId);
            }

            int totalUsers = usersQuery.Count();

            AllViewModel model = new AllViewModel()
            {
                TotalUsers = totalUsers,
                Users = users
            };

            return model;
        }
    }
}
