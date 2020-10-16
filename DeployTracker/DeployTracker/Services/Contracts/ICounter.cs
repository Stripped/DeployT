namespace DeployTracker.Services.Contracts
{
    public interface ICounter
    {
        void Increment();
        int GetValue();
    }
}
