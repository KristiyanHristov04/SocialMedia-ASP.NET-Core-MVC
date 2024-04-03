using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.CompilerServices;

namespace SocialMedia.Areas.Admin.Filters
{
    public class AdminActivityActionFilter : ActionFilterAttribute, IAsyncActionFilter
    {
        private string activity;
        public AdminActivityActionFilter(string activity)
        {
            this.activity = activity;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next.Invoke();
            await Console.Out.WriteLineAsync($"{this.activity} => {DateTime.Now}");
        }
    }
}
