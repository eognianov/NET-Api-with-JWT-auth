using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts.V1;
using WebApplication.Contracts.V1.InputModels;
using WebApplication.Contracts.V1.ViewModels;
using WebApplication.Services;

namespace WebApplication.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestInputModel request)
        {
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedViewModel
                {
                    Errors = authResponse.Errors
                });
            }
            
            return Ok(new AuthSuccessViewModel
            {
                Token = authResponse.Token
            });
        }
    }
}