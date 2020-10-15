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
        public MathTaskResult Evaluate(MathTask task)
        {
            var result = new MathTaskResult();
            switch (task.Operation)
            {
                case MathOperation.Add:
                    result.Result = task.LeftHandOperand + task.RightHandOperand;
                    break;
                case MathOperation.Subtract:
                    result.Result = task.LeftHandOperand - task.RightHandOperand;
                    break;
                case MathOperation.Multiply:
                    result.Result = task.LeftHandOperand * task.RightHandOperand;
                    break;
                case MathOperation.Divide:
                    result.Result = task.LeftHandOperand / task.RightHandOperand;
                    break;
                default:
                    break;
            }
            return result;
        }

    }
}
