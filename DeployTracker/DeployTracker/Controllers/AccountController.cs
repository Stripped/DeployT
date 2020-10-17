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

        [HttpPost]
        [Route(nameof(Login))]
        public IActionResult Login(LoginUserData loginUserData)
        {
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


        [Authorize]
        [Route("SecretArea/GetData")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }


    }
}

