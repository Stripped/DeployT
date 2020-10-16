using DeployTracker.Models;

namespace DeployTracker.Services.Contracts
{
    public interface IMathService
    {
        MathTaskResult Evaluate(MathTask task);
    }
}
