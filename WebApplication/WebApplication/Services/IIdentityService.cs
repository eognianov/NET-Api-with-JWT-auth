using System.Threading.Tasks;
using WebApplication.Domain;
namespace WebApplication.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}