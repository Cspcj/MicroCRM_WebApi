using MCRMWebApi.DTOs;

namespace MCRMWebApi.Authentication
{
    public class AuthenticateResponse
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(UserDTO user, string token)
        {
            UserID = user.Id;
            Name = user.UserName;
            Token = token;
        }
    }
}
