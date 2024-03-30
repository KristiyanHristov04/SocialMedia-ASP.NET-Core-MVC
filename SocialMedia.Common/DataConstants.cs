using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Common
{
    public static class DataConstants
    {
        public static class ApplicationUser
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 30;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 30;
        }

        public static class Post
        {
            public const int TextMaxLength = 450;
        }

        public static class FileConfiguration
        {
            public const int MaxAllowedFileSize = 3000000;
            public static HashSet<string> allowedFilesExtensions
                = new HashSet<string>()
            {
                "jpeg",
                "jpg",
                "png",
                "gif",
                "mp4"
            };
        }
    }
}
