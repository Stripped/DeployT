﻿using DeployTracker.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployTracker.Services.Concrete
{
    public class AuthOptions : IAuthOptions
    {
        public const string ISSUER = "prrudnev"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "aaaaaaaaaa2323DDDDDsdss";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}