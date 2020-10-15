using DeployTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Services
{
    interface IMathService
    {
        MathTaskResult Evaluate(MathTask task);
    }
}
