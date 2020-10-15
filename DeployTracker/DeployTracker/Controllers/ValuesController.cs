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
        [Route("api/Compute")]
        public IActionResult Compute([FromBody] MathTask task) 
        {
            double result=0;
            switch (task.Operation)
            {
                case MathOperation.Add:
                    result = task.LeftHandOperand + task.RightHandOperand;
                    break;
                case MathOperation.Subtract:
                    result = task.LeftHandOperand - task.RightHandOperand;
                    break;
                case MathOperation.Multiply:
                    result = task.LeftHandOperand * task.RightHandOperand;
                    break;
                case MathOperation.Divide:
                    result = task.LeftHandOperand / task.RightHandOperand;
                    break;
                default:
                    break;
            }
            var response = new MathTaskResult // готовим возвращаемую модельку
            {
                Result = result
            };
            return Ok(response); // возвращаем успешный ответ (код 200 OK) с нашей моделькой
        }
    }

}
