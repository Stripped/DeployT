using DeployTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Services.Contracts
{
    public interface IServerAuth
    {
        public string LoginToServer(ServerPool serverPool);
    }
}
