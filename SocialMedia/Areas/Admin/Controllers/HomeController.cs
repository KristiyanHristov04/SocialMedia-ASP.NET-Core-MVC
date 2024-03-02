using Microsoft.AspNetCore.Mvc;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Home;

namespace SocialMedia.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        private readonly IStatisticService statisticService;
        public HomeController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            StatisticsViewModel statisticsModel 
                = await this.statisticService.GetStatisticsAsync();

            return View(statisticsModel);
        }
    }
}
