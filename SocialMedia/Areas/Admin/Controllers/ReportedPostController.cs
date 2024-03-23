using Microsoft.AspNetCore.Mvc;
using SocialMedia.Areas.Admin.Services.Interfaces;
using SocialMedia.Areas.Admin.ViewModels.Post;

namespace SocialMedia.Areas.Admin.Controllers
{
    public class ReportedPostController : AdminController
    {
        private readonly IReportedPostService postService;
        public ReportedPostController(IReportedPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Preview(int id)
        {
            if (!await this.postService.ReportedPostExistsByIdAsync(id))
            {
                return BadRequest();
            }

            PreviewViewModel model = await this.postService
                .GetReportedPostPreviewInformationAsync(id);

            return View(model);
        }
    }
}
