using DeployTracker.Database;
using DeployTracker.Models;
using DeployTracker.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using DeployTracker.Services.Concrete;
using System.Text;
using System.Threading.Tasks;

namespace DeployTracker.Services.Concrete
{
    /*
JWT-токен — это некоторое хранилище, которое можно проверить сервером. В нём можно хранить некоторые пары «ключ-значение».
Плюс, там есть ещё дополнительная информация об алгоритме, издателе, потребителе, сроке и т.д.
Пользователь приходит к нам с логином и паролем. Если в системе такой юзер есть, то создаём JWT-токен.
В нём мы можем записать, например, идентификатор сессии пользователя или имя пользователя. 
Можно и больше записать, но не всегда это нужно. Юзер запоминает себе этот токен и дальше каким-либо образом передаёт нам его (например, в HTTP-заголовках).
Пользователь пришёл к нам с токеном. Из токена мы достаём идентификатор сессии (или имя пользователя), при этом ещё проверили, что токен не протух, что он валидный и т.д.
По этим данным строим identity/principal с claim'ами, которые уже нужны нам в приложении (например, роли пользователя) и отдаём из хендлера этот тикет.
Тут может запутать то, что claim'ы в токене не обязательно совпадают с claim'ами пользователя (хотя и могут).
     */
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
