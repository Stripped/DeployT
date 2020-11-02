using Microsoft.IdentityModel.Tokens;

namespace DeployTracker.Services.Contracts
{
    public interface IAuthOptions
    {
        SymmetricSecurityKey GetSymmetricSecurityKey();
        TokenValidationParameters GetTokenValidationParameters();
    }
}
