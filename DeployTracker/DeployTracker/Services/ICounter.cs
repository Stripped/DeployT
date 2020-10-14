using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Services
{
    public interface ICounter
    {
        void Increment();
        int GetValue();
    }
}
