using DeployTracker.Models;
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
        public AuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, // сюда в т.ч. можно дополнительно инъецировать JWT-сервис
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // здесь есть доступ к Request, в т.ч. к Request.Headers, в которых
            // может передаваться токен
            if (Request.Headers["MyJwtAuthHeader"].ToString()!=null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Wrong password? :( "));
            }
            var claims = new List<Claim>
        {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, User.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role)
        };
            // готовим тикет для успешной авторизации
            var identity = new ClaimsIdentity(claims, "MyScheme");  // "MyScheme" всюду нужно вынести в константу, хардкорный стринг — плохо
            var principle = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principle, "MyScheme");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
