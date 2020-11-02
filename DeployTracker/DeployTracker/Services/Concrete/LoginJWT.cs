using DeployTracker.Database;
using DeployTracker.Models;
using DeployTracker.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DeployTracker.Services.Concrete
{
    public class LoginJWT: ILoginJWT
    {
        private readonly IAuthOptions _authOptions;
        private readonly IUserRepository _userRepository;
        
        public LoginJWT(IAuthOptions authOptions, IUserRepository userRepository)
        {
            _authOptions = authOptions;
            _userRepository = userRepository;
        }

        public IEnumerable<Claim> GetClaims(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
                throw new ArgumentException("Given token null or empty");
            TokenValidationParameters tokenValidationParameters = _authOptions.GetTokenValidationParameters();
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out SecurityToken validatedToken);
                return tokenValid.Claims;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public string Login(LoginUserData loginUserData)
        {
            var identity = GetIdentity(loginUserData.Login, loginUserData.Password);
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }



        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User User = _userRepository.GetUserByLoginPassword(username, password);
            if (User != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, User.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
