using System.Threading.Tasks;

namespace DeployTracker.Services.Contracts
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
