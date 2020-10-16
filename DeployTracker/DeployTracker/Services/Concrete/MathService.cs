using System;
using DeployTracker.Models;
using DeployTracker.Services.Contracts;

namespace DeployTracker.Services.Concrete
{
    public class MathService: IMathService
    {
        public MathTaskResult Evaluate(MathTask task)
        {
            var result = new MathTaskResult
            {
                Result = task.Operation switch {
                    MathOperation.Add => 
                        task.LeftHandOperand + task.RightHandOperand,
                    MathOperation.Subtract => 
                        task.LeftHandOperand - task.RightHandOperand,
                    MathOperation.Multiply => 
                        task.LeftHandOperand * task.RightHandOperand,
                    MathOperation.Divide => 
                        task.LeftHandOperand / task.RightHandOperand,
                    _ => throw new ArgumentOutOfRangeException()
                }
            };

            return result;
        }

    }
}
