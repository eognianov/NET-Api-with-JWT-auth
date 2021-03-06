using System.ComponentModel.DataAnnotations;

namespace WebApplication.Contracts.V1.InputModels
{
    public class UserRegistrationRequestInputModel
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}