using DeployTracker.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeployTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICounter _counter;
        public HomeController(ILogger<HomeController> logger, ICounter counter)
        {
            _logger = logger;
            _counter = counter;
        }

        public ActionResult Index()
        {
            _logger.LogInformation("Index action called!");
            _logger.LogInformation(_counter.GetValue().ToString());
            _counter.Increment();
            return View();
        }
    }
}