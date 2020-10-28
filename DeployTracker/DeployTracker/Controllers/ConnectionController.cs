using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeployTracker.Models;
using DeployTracker.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeployTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IServerAuth _serverAuth;
        public ConnectionController(IServerAuth serverAuth)
        {
            _serverAuth = serverAuth;
        }

        [HttpPost]
        [Route(nameof(Connect))]
        public async Task<List<string>> Connect(List<ServerPool> serversList)
        {
            List<string> taskResult = new List<string>();
            foreach (var item in serversList)
            {
                var LastTask = await _serverAuth.LoginToServerAsync(item);
                taskResult.Add(LastTask);
            }

            return taskResult;
        }
    }
}
