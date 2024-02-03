using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }
    }
}
