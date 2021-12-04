using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Domain;

namespace WebApplication.Services
{
    public interface IPostService
    {
        List<Post> GetPosts();
        Post GetPostById(Guid id);

        void Add(Post post);
    }
}