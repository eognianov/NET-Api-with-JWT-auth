using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Authorization;

public class WorksForCompanyHandler : AuthorizationHandler<WorksForCompanyRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WorksForCompanyRequirement requirement)
    {
        var userEmail = context.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        if (userEmail.EndsWith(requirement.DomainName))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        context.Fail();
        return Task.CompletedTask;
    }
}