using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;
using WebApplication.Contracts.V1;
using WebApplication.Contracts.V1.InputModels;
using WebApplication.Contracts.V1.ViewModels;
using WebApplication.Domain;
using WebApplication.Services;

namespace WebApplication.Controllers.V1
{
    public class PostsController: Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postService.GetPosts());
        }
        
        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            var result = _postService.GetPostById(postId);
            if (result == null)
            {
                return NotFound();
            }

            var model = new PostViewModel {Id = result.Id, Name = result.Name};

            return Ok(model);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] PostInputModel postRequest)
        {
            var post = new Post {Id = postRequest.Id};
            
            if (post.Id != Guid.Empty)
            {
                post.Id = Guid.NewGuid();
            }
            
            _postService.Add(post);

            // var location = ApiRoutes.Posts.getPostRoute(post.Id);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{id}", post.Id.ToString());
            var postViewModel = new PostViewModel {Id = post.Id};
            return Created(locationUri, postViewModel);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId,[FromBody] UpdatePostInputModel postToUpdate)
        {
            var post = new Post {Id = postId, Name = postToUpdate.Name};

            var updated = _postService.Update(post);
            if (updated)
            {
                var model = new PostViewModel {Id = postId, Name = postToUpdate.Name};
                return Ok(model);
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public IActionResult Delete([FromRoute] Guid postId)
        {
            if (_postService.Delete(postId))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}