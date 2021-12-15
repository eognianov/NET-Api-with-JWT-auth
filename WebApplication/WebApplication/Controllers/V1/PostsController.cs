using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts.V1;
using WebApplication.Contracts.V1.InputModels;
using WebApplication.Contracts.V1.ViewModels;
using WebApplication.Domain;
using WebApplication.Services;

namespace WebApplication.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController: Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetPostsAsync());
        }
        
        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var result = await _postService.GetPostByIdAsync(postId);
            if (result == null)
            {
                return NotFound();
            }

            var model = new PostViewModel {Id = result.Id, Name = result.Name};

            return Ok(model);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] PostInputModel postRequest)
        {

            var post = new Post {Name = postRequest.Name};

            var added = await _postService.CreatePostAsync(post);

            if (!added)
            {
                return BadRequest();
            }
            
            // var location = ApiRoutes.Posts.getPostRoute(post.Id);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());
            var postViewModel = new PostViewModel {Id = post.Id, Name = post.Name};
            return Created(locationUri, postViewModel);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId,[FromBody] UpdatePostInputModel postToUpdate)
        {
            var post = new Post {Id = postId, Name = postToUpdate.Name};

            var updated = await _postService.UpdatePostAsync(post);
            if (updated)
            {
                var model = new PostViewModel {Id = postId, Name = postToUpdate.Name};
                return Ok(model);
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            if (await _postService.DeletePostAsync(postId))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}