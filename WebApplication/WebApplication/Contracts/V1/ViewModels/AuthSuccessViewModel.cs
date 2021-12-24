using WebApplication.Domain;

namespace WebApplication.Contracts.V1.ViewModels
{
    public class AuthSuccessViewModel
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}