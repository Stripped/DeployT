using DeployTracker.Services.Contracts;

namespace DeployTracker.Services.Concrete
{
    public class Counter : ICounter
    {
        private int _counter = 0;
        public void Increment() => ++_counter;
        public int GetValue() => _counter;
    }
}
