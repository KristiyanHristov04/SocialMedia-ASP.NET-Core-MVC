using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Country;

namespace SocialMedia.Services
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext context;
        public CountryService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<CountryViewModel>> GetAllCountriesAsync()
        {
            return await this.context.Countries.Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
              .ToListAsync();
        }
    }
}
