using MCRMWebApi.Authentication;

namespace MCRMWebApi.Services
{
    public interface IAuthenticationService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
