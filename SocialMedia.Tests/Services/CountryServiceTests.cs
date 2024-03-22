using SocialMedia.Services;
using SocialMedia.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Tests.Services
{
    public class CountryServiceTests : BaseTest
    {
        private ICountryService countryService;
        public CountryServiceTests()
        {
            this.countryService = new CountryService(this.context);
        }

        [Fact]
        public async Task GetAllCountriesAsync_ReturnsCorrectCount()
        {
            var result = await this.countryService.GetAllCountriesAsync();

            int resultCount = result.Count();

            Assert.Equal(2, resultCount);
        }

        [Fact]
        public async Task GetAllCountriesAsync_ReturnsCorrectData()
        {
            var result = await this.countryService.GetAllCountriesAsync();

            var country = result.First();

            Assert.Equal(1, country.Id);
            Assert.Equal("Bulgaria", country.Name);
        }
    }
}
