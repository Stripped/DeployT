using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Services.Contracts
{
    public interface IAuthOptions
    {
        SymmetricSecurityKey GetSymmetricSecurityKey();
        TokenValidationParameters GetTokenValidationParameters();
    }
}
