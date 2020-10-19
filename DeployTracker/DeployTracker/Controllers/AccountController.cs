﻿using DeployTracker.Models;
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
    [Authorize(AuthenticationSchemes = "MyScheme")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: Controller
    {
        private readonly IAuthOptions _authOptions;
        private readonly ILoginJWT _loginJWT;

        public AccountController(IAuthOptions authOptions,ILoginJWT loginJWT)
        {
            _authOptions = authOptions;
            _loginJWT = loginJWT;
        }

        [HttpPost]
        [Route(nameof(Login))]
        public IActionResult Login(LoginUserData loginUserData)
        {
            TokenResponse response;
            var token = _loginJWT.Login(loginUserData);
            
            if (token != null)
            {
                response = new TokenResponse
                {
                    Success = true,
                    ErrorMessage = "",
                    Data = token
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

