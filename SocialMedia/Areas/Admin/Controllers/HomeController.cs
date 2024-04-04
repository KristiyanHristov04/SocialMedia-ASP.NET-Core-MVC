using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Home;
using SocialMedia.Models;
using System.Diagnostics;

namespace SocialMedia.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        private readonly IStatisticService statisticService;
        private readonly IMemoryCache memoryCache;
        public HomeController(
            IStatisticService statisticService,
            IMemoryCache memoryCache)
        {
            this.statisticService = statisticService;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            StatisticsViewModel statistics;

            if (!this.memoryCache.TryGetValue("Stats", out statistics))
            {
                statistics = await this.statisticService.GetStatisticsAsync();
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

                this.memoryCache.Set("Stats", statistics, options);
            }

            return View(statistics);
        }
    }
}
