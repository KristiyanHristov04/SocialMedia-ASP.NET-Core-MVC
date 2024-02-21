using SocialMedia.ViewModels.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryViewModel>> GetAllCountriesAsync();
    }
}
