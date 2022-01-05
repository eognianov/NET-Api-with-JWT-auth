using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts.V1;
using WebApplication.Services;

namespace WebApplication.Controllers.V1;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TagsController: Controller
{
    private readonly IPostService _postService;

    public TagsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet(ApiRoutes.Tags.GetAll)]
    [Authorize(Policy = "TagViewer")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(new []{"tag1", "tag2"});
    }
}