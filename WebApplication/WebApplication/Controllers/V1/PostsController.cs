using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;
using WebApplication.Contracts.V1;
using WebApplication.Contracts.V1.InputModels;
using WebApplication.Contracts.V1.ViewModels;
using WebApplication.Domain;

namespace WebApplication.Controllers.V1
{
    public class PostsController: Controller
    {
        private List<Post> _posts;

        public PostsController()
        {
            _posts = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post(){Id = Guid.NewGuid().ToString()});
            }
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_posts);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] PostInputModel postRequest)
        {
            var post = new Post {Id = postRequest.Id};
            
            if (string.IsNullOrEmpty(post.Id))
            {
                post.Id = Guid.NewGuid().ToString();
            }
            
            _posts.Add(post);

            // var location = ApiRoutes.Posts.getPostRoute(post.Id);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{id}", post.Id);
            var postViewModel = new PostViewModel {Id = post.Id};
            return Created(locationUri, postViewModel);
        }
        
    }
}