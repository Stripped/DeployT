using DeployTracker.Database;
using DeployTracker.Models;
using DeployTracker.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DeployTracker.Handler
{
    public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        const string schemeName = "MyScheme";
        private readonly ILoginJWT _loginJWT;
        private readonly IUserRepository _userRepository;
        public AuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ILoginJWT loginJWT,
        IUserRepository userRepository) : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
            _loginJWT = loginJWT;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers["MyJwtAuthHeader"].ToString()==null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Wrong password? :( "));
            }
            var claims = new List<Claim>
        {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, _userRepository.),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role)
        };
            // готовим тикет для успешной авторизации
            var identity = new ClaimsIdentity(claims, schemeName);
            var principle = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principle, schemeName);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
