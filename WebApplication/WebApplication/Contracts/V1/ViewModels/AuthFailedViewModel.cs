using System.Collections.Generic;

namespace WebApplication.Contracts.V1.ViewModels
{
    public class AuthFailedViewModel
    {
        public IEnumerable<string> Errors { get; set; }
    }
}