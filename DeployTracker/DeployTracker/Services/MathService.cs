using DeployTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Services
{
    public class MathService: IMathService
    {
        public string ResultText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double ResultDouble { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MathService()
        {
            ResultText = "result";
            ResultDouble = new MathTaskResult().Result;
        }

        public string GetValue()
        {
            MathService mathService = new MathService();
            var result = JsonConvert.SerializeObject(mathService);
            return result;
        }

    }
}
