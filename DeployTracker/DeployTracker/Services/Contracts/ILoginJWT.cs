using DeployTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeployTracker.Services.Contracts
{
    public interface ILoginJWT
    {
        public string Login(LoginUserData loginUserData);
        IEnumerable<Claim> GetClaims(string jwtToken);

    }
}
