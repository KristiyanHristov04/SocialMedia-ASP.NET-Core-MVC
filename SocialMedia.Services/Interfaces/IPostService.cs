﻿using SocialMedia.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services.Interfaces
{
    public interface IPostService
    {
        Task AddPostAsync(PostFormModel model, string userId);
        Task EditPostAsync(int id, PostFormModel model);
        Task<PostFormModel> GetPostByIdAsync(int id);
        Task<bool> ValidatePostUserAsync(string userId, int postId);
    }
}
