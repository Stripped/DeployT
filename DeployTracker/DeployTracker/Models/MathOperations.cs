using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeployTracker.Models
{
    public enum MathOperation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
    public class MathTask
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))] // зачем оно нужно? :)
        public MathOperation Operation { get; set; }
        public double LeftHandOperand { get; set; }
        public double RightHandOperand { get; set; }
        
        public MathTask(MathOperation operation,double left,double right)
        {
            Operation = operation;
            LeftHandOperand = left;
            RightHandOperand = right;
        }
    }
    public class MathTaskResult
    {
        public double Result { get; set; }

    }
}
