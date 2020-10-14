using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeployTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeployTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost]
        [Route("api/Calculate")]
        public double JsonDataPost([FromBody] MathTask content)
        {
           var mathTaskObj = new MathTask(content.Operation, content.LeftHandOperand, content.RightHandOperand);
           var mathTaskResult = new MathTaskResult(mathTaskObj);
           return mathTaskResult.Result;
        }
    }

}
