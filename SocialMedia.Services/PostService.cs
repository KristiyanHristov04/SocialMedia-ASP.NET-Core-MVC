using Microsoft.AspNetCore.Hosting;
using SocialMedia.Data;
using SocialMedia.Data.Models;
using SocialMedia.Services.Interfaces;
using SocialMedia.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public PostService(
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task AddPostAsync(PostFormModel model, string userId)
        {
            string filesFolderPath = "files/" + Guid.NewGuid().ToString() + "_" + model.File.FileName;
            string fullPath = Path.Combine(webHostEnvironment.WebRootPath, filesFolderPath);
            //await model.File.CopyToAsync(new FileStream(fullPath, FileMode.Create));

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            Post post = new Post()
            {
                Text = model.Text,
                Path = filesFolderPath,
                UserId = userId
            };

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();   
        }
    }
}
