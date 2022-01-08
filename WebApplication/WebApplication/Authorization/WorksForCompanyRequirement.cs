using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Authorization;

public class WorksForCompanyRequirement : IAuthorizationRequirement
{
    public string DomainName { get; set; }

    public WorksForCompanyRequirement(string domainName)
    {
        DomainName = domainName;
    }
}