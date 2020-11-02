using DeployTracker.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace DeployTracker.Services.Contracts
{
    public interface ILoginJWT
    {
        public string Login(LoginUserData loginUserData);
        IEnumerable<Claim> GetClaims(string jwtToken);

    }
}
