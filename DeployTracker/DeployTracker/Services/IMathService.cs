using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Services
{
    interface IMathService
    {
        public string ResultText { get;set; }
        public double ResultDouble { get;set; }

        public string GetValue();
    }
}
