using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Models
{
    public class TokenResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string Data { get; set; }
    }
}
