using DeployTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeployTracker.Services.Concrete
{
    public class LoginJWT
    {
        private List<User> people = new List<User>
        {
            new User {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new User { Login="qwerty@gmail.com", Password="55555", Role = "user" }
        };
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
            User User = people.FirstOrDefault(x => x.Login == username && x.Password == password);
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
