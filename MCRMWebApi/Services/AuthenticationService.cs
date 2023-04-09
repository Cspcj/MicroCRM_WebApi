using MCRMWebApi.Authentication;
using MCRMWebApi.DataContext;
using MCRMWebApi.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MCRMWebApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly MCRMDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(MCRMDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x=> x.UserName == model.Username && x.Password == model.Password);
            if (user == null) { return null; }

            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);   
        }

        private string generateJwtToken(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AUTHSECRET_AUTHSECRET"));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration.GetValue<string>("Authentication:Domain")
                , _configuration.GetValue<string>("Authentication:Audience")
                , null
                , expires: DateTime.Now.AddDays(3)
                ,signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
