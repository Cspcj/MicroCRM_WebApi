using System.ComponentModel.DataAnnotations;

namespace MCRMWebApi.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; } 
        [Required]
        public string Password { get; set; }
    }
}
