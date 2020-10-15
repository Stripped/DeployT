using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeployTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeployTracker.Services;
using Microsoft.Extensions.Logging;

namespace DeployTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        public ValuesController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        [Route("Compute")]
        public IActionResult Compute([FromBody] MathTask task) 
        {
            MathTaskResult response = new MathService().Evaluate(task); // сервисом обработали полученный объект
            _logger.LogInformation(response.Result.ToString());
            return Ok(response); // возвращаем успешный ответ (код 200 OK) с нашей моделькой
        }
    }

}
