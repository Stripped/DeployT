using DeployTracker.Models;
using DeployTracker.Services.Concrete;
using DeployTracker.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace DeployTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: Controller
    {
        private readonly IAuthOptions _authOptions;

        public AccountController(IAuthOptions authOptions)
        {
            _authOptions = authOptions;
        }
        // тестовые данные вместо использования базы данных
        private List<User> people = new List<User>
        {
            new User {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new User { Login="qwerty@gmail.com", Password="55555", Role = "user" }
        };
 
        [HttpPost]
        [Route(nameof(Login))]
        public IActionResult Login(LoginUserData loginUserData)
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
            TokenResponse response;
            if (identity!=null)
            {
                response = new TokenResponse
                {
                    Success = true,
                    ErrorMessage = "",
                    Data = encodedJwt
                };
            }
            else
            {
                response = new TokenResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid username or password.",
                    Data = null
                };
            }
            return Json(response);
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

        [Authorize]
        [Route("SecretArea/GetData")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }


    }
}

