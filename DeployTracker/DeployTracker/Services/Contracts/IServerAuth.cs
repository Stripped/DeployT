﻿using DeployTracker.Models;
using System.Threading.Tasks;

namespace DeployTracker.Services.Contracts
{
    public interface IServerAuth
    {
        Task<string> LoginToServerAsync(ServerPool serverPool);
    }
}
