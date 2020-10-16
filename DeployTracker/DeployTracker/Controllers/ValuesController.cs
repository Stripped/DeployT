using DeployTracker.Models;
using Microsoft.AspNetCore.Mvc;
using DeployTracker.Services.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace DeployTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMathService _mathService;
        
        public ValuesController(ILogger<HomeController> logger, 
            IMathService mathService)
        {
            _logger = logger;
            _mathService = mathService;
        }

        [HttpPost]
        [Route(nameof(Compute))]
        public IActionResult Compute([FromBody] MathTask task) 
        {
            var result = _mathService.Evaluate(task);
            _logger.LogInformation($"The operation was {task.Operation} with operands {task.LeftHandOperand} and {task.RightHandOperand}, " +
                                   $"the result was {result.Result}.");
            return Ok(result);
        }

        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Ваша роль: администратор");
        }
    }

}
