using System.Text.Json.Serialization;

namespace DeployTracker.Models
{
    public class MathTask
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MathOperation Operation { get; set; }
        public double LeftHandOperand { get; set; }
        public double RightHandOperand { get; set; }
    }
}